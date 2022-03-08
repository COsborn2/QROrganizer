<template>
  <v-card class="mx-auto" width="550px">
    <cancel-save-modal
      v-model="editContainerModal"
      header-text="Edit Container"
      :isLoading="container.$save.isLoading"
      v-on:saveClicked="saveClicked"
    >
      <v-text-field
          class="mt-5"
          color="primary"
          placeholder="Item Name"
          label="Item Name"
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
            Delete Container
          </v-list-item>
          <v-list-item @click="editContainerModal = true">
            Edit Container Title
          </v-list-item>
        </v-list>
      </v-menu>
    </v-card-title>
    <v-card-text>
      <v-dialog transition="dialog-bottom-transition" max-width="600">
        <template v-slot:activator="{ on, attrs }">
          <!-- TODO: Remove this button and add to v-speed-dialo -->
          <v-btn
            class="mt-1 mb-1"
            large
            color="success"
            style="width: 100%"
            v-bind="attrs"
            v-on="on"
            @click="resetItemBeingAdded"
          >
            Add Item
          </v-btn>
        </template>
        <template v-slot:default="dialog">
          <v-card>
            <v-toolbar color="primary" dark>
              Add New Item to Container
            </v-toolbar>
            <v-card-text>
              <v-text-field
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
            </v-card-text>
            <v-card-actions class="justify-end">
              <v-btn text @click="dialog.value = false"
              >Close</v-btn>
              <v-btn color="success" @click="saveItem(dialog)">Save</v-btn>
            </v-card-actions>
          </v-card>
        </template>
      </v-dialog>
      <c-loader-status :loaders="{ '': [itemListVM.$load] }" #default>
        <template v-if="noItems">
          <h2 class="text-center">
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

  @Prop({ type: Object, required: true })
  container!: ContainerViewModel;

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

  async saveItem(dialog: any) {
    this.itemVM.containerId = this.container.id;
    this.itemVM.userId = this.container.userId;
    await this.itemVM.$save();

    if (this.itemVM.$save.wasSuccessful === true) {
      dialog.value = false;
      await this.itemListVM.$load();
      this.noItems = this.itemListVM.$items.length < 1;
    }
  }

  resetItemBeingAdded() {
    this.itemVM = new ItemViewModel();
    this.itemVM.quantity = 1;
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
