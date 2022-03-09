<template>
  <v-card class="mx-auto" width="550px">
    <cancel-save-modal
      v-model="editContainerModal"
      header-text="Edit Container"
      :isLoading="container.$save.isLoading"
      v-on:saveClicked="saveClicked"
    >
      <v-text-field
        autofocus
        class="mt-5"
        color="primary"
        placeholder="Container Name"
        label="Container Name"
        type="text"
        v-model="container.containerName"
      ></v-text-field>
    </cancel-save-modal>
    <delete-confirmation-modal
      :is-loading="container.$delete.isLoading"
      v-model="deleteConfirmationModal"
      confirmation-text="Are you sure you want to delete this container?"
      v-on:deleteClicked="deleteClicked"
    ></delete-confirmation-modal>
    <cancel-save-modal
      v-model="addItemModal"
      header-text="Add Item to Container"
      :is-loading="itemVM.$save.isLoading"
      v-on:saveClicked="saveItem"
    >
      <v-text-field
        autofocus
        class="mt-5"
        color="primary"
        placeholder="Item Name"
        label="Item Name"
        type="text"
        v-model="itemVM.name"
      ></v-text-field>
      <v-text-field
        color="primary"
        placeholder="Quantity"
        label="Quantity"
        type="number"
        v-model="itemVM.quantity"
      ></v-text-field>
    </cancel-save-modal>
    <v-card-title>
      <span class="text-h4 font-weight-light" style="margin-right: 10%">
        {{ container.containerName }}
      </span>
      <v-spacer></v-spacer>
      <v-menu
        bottom
        left
        offset-y
        transition="slide-y-transition"
        rounded="b-xl"
      >
        <template v-slot:activator="{ on, attrs }">
          <v-btn
            color="blue darken-2"
            absolute
            top
            right
            icon
            v-bind="attrs"
            v-on="on"
          >
            <v-icon color="secondary">
              fa-solid fa-cog
            </v-icon>
          </v-btn>
        </template>
        <v-list>
          <v-list-item @click="deleteConfirmationModal = true">
            <v-list-item-icon>
              <v-icon>fa fa-trash</v-icon>
            </v-list-item-icon>
            Delete Container
          </v-list-item>
          <v-list-item @click="editContainerModal = true">
            <v-list-item-icon>
              <v-icon>fa fa-edit</v-icon>
            </v-list-item-icon>
            Edit Container
          </v-list-item>
          <v-list-item @click="addItem">
            <v-list-item-icon>
              <v-icon>fa fa-plus</v-icon>
            </v-list-item-icon>
            Add Item
          </v-list-item>
        </v-list>
      </v-menu>
    </v-card-title>
    <v-card-text>
      <c-loader-status :loaders="{ '': [itemListVM.$load] }" #default>
        <template v-if="noItems">
          <v-divider />
          <h2 class="mt-3 text-center">
            No Items
          </h2>
          <v-row class="mb-1">
            <v-col class="text-center">
              <v-btn color="success" @click="reload">
                Reload
                <v-icon small>fas fa-sync</v-icon>
              </v-btn>
            </v-col>
          </v-row>
        </template>
        <template v-else>
          <v-text-field
              type="text"
              label="Search items"
              class="mb-n5 mt-1"
              v-model="itemListVM.$params.search"
          ></v-text-field>
          <v-simple-table fixed-header height="300px">
            <template v-slot:default>
              <thead>
              <tr>
                <th class="text-left">
                  Item Name
                </th>
                <th class="text-left">
                  Quantity
                </th>
              </tr>
              </thead>
              <tbody>
              <tr v-for="item in itemListVM.$items">
                <td>{{ item.name }}</td>
                <td>{{ item.quantity }}</td>
              </tr>
              </tbody>
            </template>
          </v-simple-table>
        </template>
      </c-loader-status>
      <v-divider />
      <v-row class="mt-1">
        <v-col class="text-center">
          <c-list-range-display style="font-size: 20px" :list="itemListVM"
          ></c-list-range-display>
        </v-col>
      </v-row>
      <v-row class="mt-n5" align="center">
        <v-col>
          <c-list-page-size :list="itemListVM" :items="[10, 20, 50]"/>
        </v-col>
        <v-col>
          <c-list-page :list="itemListVM" />
        </v-col>
      </v-row>
    </v-card-text>
  </v-card>
</template>

<script lang="ts">
import {Component, Prop, Vue, Watch} from 'vue-property-decorator';
import {ContainerViewModel, ItemListViewModel, ItemViewModel} from "@/viewmodels.g";
import {Item} from "@/models.g";
import ItemsInContainer = Item.DataSources.ItemsInContainer;
import DeleteConfirmationModal from "@/components/DeleteConfirmationModal.vue";
import CancelSaveModal from "@/components/CancelSaveModal.vue";

@Component({
  components: {CancelSaveModal, DeleteConfirmationModal}
})
export default class ContainerCard extends Vue {
  itemListVM = new ItemListViewModel();
  itemVM = new ItemViewModel();
  noItems = false;
  editContainerModal = false;
  deleteConfirmationModal = false;
  addItemModal = false;

  @Prop({ type: Object, required: true })
  container!: ContainerViewModel;

  addItem() {
    this.itemVM = new ItemViewModel();
    this.itemVM.quantity = 1;
    this.addItemModal = true;
  }

  async saveClicked() {
    await this.container.$save()
    this.editContainerModal = false;
  }

  async deleteClicked() {
    await this.container.$delete();
    this.$emit('containerRemoved', this.container.id);
    this.deleteConfirmationModal = false;
  }

  async reload() {
    await this.itemListVM.$load()
    this.noItems = (this.itemListVM.$load.totalCount ?? 0) < 1;
  }

  @Watch('itemListVM.$pageSize')
  @Watch('itemListVM.$page')
  @Watch('itemListVM.$params.search')
  loadItems() {
    this.itemListVM.$load();
  }

  async saveItem() {
    this.itemVM.containerId = this.container.id;
    this.itemVM.userId = this.container.userId;
    await this.itemVM.$save();

    if (this.itemVM.$save.wasSuccessful === true) {
      this.addItemModal = false;
      await this.itemListVM.$load();
      this.noItems = this.itemListVM.$items.length < 1;
    }
  }

  async mounted() {
    this.container.$params.includes = 'none';

    let dataSource = new ItemsInContainer();
    dataSource.containerId = this.container.id;
    this.itemListVM.$dataSource = dataSource;
    this.itemListVM.$load.setConcurrency('debounce');
    await this.itemListVM.$load();
    this.noItems = (this.itemListVM.$load.totalCount ?? 0) < 1;
  }
}
</script>

<style lang="scss" scoped>

</style>
