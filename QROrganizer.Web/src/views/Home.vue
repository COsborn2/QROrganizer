<template>
  <div>
    <div v-if="allLoadedContainers.length < 1 && !isLoading" class="d-flex justify-center primary-background" style="height: 100%; width: 100%">
      <v-container
          fill-height
          style="position: fixed; max-width: 600px"
          class="d-flex align-center justify-center text-center align-center"
      >
        <h1>No Containers! Try adding one below!</h1>
        <v-icon class="bounce-up-down" x-large style="position: fixed; bottom: 15%; right: 35px; font-size: 60px" color="primary">
          fas fa-long-arrow-alt-down
        </v-icon>
      </v-container>
    </div>
    <v-fab-transition>
      <v-btn
        color="primary"
        dark
        fixed
        bottom
        right
        fab
        x-large
        @click="addContainerButtonClicked"
      >
        <v-icon>fa-solid fa-plus</v-icon>
      </v-btn>
    </v-fab-transition>
    <div class="home mb-16">
      <cancel-save-modal
          v-model="addContainerModal"
          header-text="Create New Container"
          :isLoading="isLoading"
          v-on:saveClicked="saveNewModal"
          v-if="containerBeingAdded"
      >
        <v-text-field
          autofocus
          class="mt-5"
          color="primary"
          placeholder="Item Name"
          label="Item Name"
          type="text"
          v-model="containerBeingAdded.containerName"
        ></v-text-field>
      </cancel-save-modal>

      <v-row justify="space-around" no-gutters>
        <v-col class="ma-2" v-for="container in allLoadedContainers" :key="container.id">
          <container-card
              :container="container"
              v-on:containerRemoved="containerRemoved"
          ></container-card>
        </v-col>
      </v-row>
      <v-row justify="center">
        <v-progress-circular v-if="containerService.$load.isLoading" :size="100" color="primary" indeterminate></v-progress-circular>
      </v-row>
    </div>
  </div>
</template>

<script lang="ts">
import {Component, Vue} from 'vue-property-decorator';
import ContainerCard from "@/components/ContainerCard.vue";
import {ContainerListViewModel, ContainerViewModel} from "@/viewmodels.g";
import CancelSaveModal from "@/components/CancelSaveModal.vue"; // @ is an alias to /src

@Component({
  components: {
    ContainerCard,
    CancelSaveModal
  },
})
export default class Home extends Vue {
  containerService = new ContainerListViewModel();
  allLoadedContainers: ContainerViewModel[] = [];
  addContainerModal = false;
  containerBeingAdded: ContainerViewModel | null = null;

  addContainerButtonClicked() {
    this.containerBeingAdded = new ContainerViewModel();
    this.containerBeingAdded.userId = 'bogusIdToPassValidation';
    this.addContainerModal = true;
  }

  get isLoading(): boolean {
    return (this.containerBeingAdded?.$save?.isLoading ?? false) || (this.containerService?.$load?.isLoading ?? false);
  }

  async saveNewModal() {
    if (this.containerBeingAdded) {
      await this.containerBeingAdded.$save();
      this.allLoadedContainers = [this.containerBeingAdded].concat(this.allLoadedContainers);
    }

    this.addContainerModal = false;
  }

  containerRemoved(containerId: number) {
    let indexOf = this.allLoadedContainers.findIndex(x => x.id === containerId);

    if (indexOf > -1) {
      this.allLoadedContainers.splice(indexOf, 1);
    }
  }

  isWithin20PercentOfBottomOfPage(): boolean {
    let twentyPercentOfPage = document.documentElement.offsetHeight / 5;
    return document.documentElement.scrollTop + window.innerHeight
        >= (document.documentElement.offsetHeight - twentyPercentOfPage)
  }

  async loadItemsToFillPage() {
    while (!(document.documentElement.scrollHeight > document.documentElement.clientHeight + 1000)) {
      if (!this.containerService.$hasNextPage) return;
      await this.loadMoreContainers();
    }
  }

  async mounted() {
    this.containerService.$includes = 'none';
    this.containerService.$pageSize = 10;
    this.containerService.$load.setConcurrency('debounce');
    await this.containerService.$load();
    this.addToLoadedContainersList();

    this.loadItemsToFillPage();

    window.onresize = () => {
      this.loadItemsToFillPage();
    }

    window.onscroll = () => {
      if (this.isWithin20PercentOfBottomOfPage()) {
        this.loadMoreContainers()
      }
    }
  }

  addToLoadedContainersList() {
    for (const container of this.containerService.$items) {
      this.allLoadedContainers.push(container);
    }
  }

  async loadMoreContainers() {
    if (!this.containerService.$hasNextPage || this.containerService.$load.isLoading) return;
    this.containerService.$nextPage();
    await this.containerService.$load();
    this.addToLoadedContainersList();
  }

  get message(): string {
    return this.$store.state.user.email;
  }
}
</script>
