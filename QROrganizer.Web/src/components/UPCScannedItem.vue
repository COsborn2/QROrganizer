<template>
  <v-alert v-if="alertOpen" style="width: 100%" :color="itemColor" :dismissible="itemColor === 'info'" dense>
    <v-row no-gutters>
      <v-progress-linear
        rounded
        :value="indeterminateLoader ? 0 : percent"
        :indeterminate="isLoading && indeterminateLoader"
        color="accent"
      />
    </v-row>
    <v-row class="text-center mt-1" align="center" no-gutters>
      <span style="width: 100%">
        {{ text }}
      </span>
    </v-row>
  </v-alert>
</template>

<script lang="ts">
import {Component, Prop, VModel, Vue} from 'vue-property-decorator';
import {ItemViewModel} from "@/viewmodels.g";

@Component({})
export default class UPCScannedItem extends Vue {
  @VModel({ type: ItemViewModel, required: true })
  item!: ItemViewModel;

  alertOpen = true;

  itemColor: 'info' | 'success' | 'error' = 'info'
  loadInterval: any | null = null;
  timeoutInterval: any | null = null;

  indeterminateLoader: boolean = true;
  timeRemainingMS: number = 0;
  timeoutSeconds = 5;

  searchIntervalSeconds = 5;

  get percent(): number {
    return ((this.timeRemainingMS) / (this.timeoutSeconds * 1000)) * 100;
  }

  get text(): string {
    if (this.itemColor === 'info') return this.item.upcCode ?? '';

    if (this.itemColor === 'error') return 'Error finding UPC Code';

    return 'Item name updated';
  }

  get isLoading(): boolean {
    return (this.item.$load.isLoading ?? false) || (this.item.$save.isLoading ?? false);
  }

  async barcodeFound() {
    if (!this.item.itemBarcodeInformation) return;

    if (this.item.itemBarcodeInformation.wasSuccessful === false) {
      this.itemColor = 'error';
      this.startTimeoutInterval();
      return;
    }

    this.item.name = this.item.itemBarcodeInformation.title;
    this.item.$save();
    this.itemColor = 'success';
    this.startTimeoutInterval();
  }

  async created() {
    this.timeRemainingMS = this.timeoutSeconds * 1000;

    if (this.item.itemBarcodeInformation) {
      this.barcodeFound();
      return;
    }

    this.item.startSearchingForUpcCode.invoke();

    this.loadInterval = setInterval(async () => {
      await this.item.$load();

      if (this.item.itemBarcodeInformation) {
        await this.barcodeFound();
        clearInterval(this.loadInterval);
      }
    }, this.searchIntervalSeconds * 1000);
  }

  async searchForBarcodeInfo(): Promise<void> {
    await this.item.$load();

    if (this.item.itemBarcodeInformation) {
      await this.barcodeFound();
    }
  }

  beforeDestroy() {
    if (this.loadInterval) {
      clearInterval(this.loadInterval);
    }
  }

  startTimeoutInterval() {
    this.indeterminateLoader = false;
    this.timeoutInterval = setInterval(async () => {
      if (this.timeRemainingMS < 0) {
        this.alertOpen = false;
        clearInterval(this.timeoutInterval);
        return;
      }

      this.timeRemainingMS -= 500;
    }, 500);
  }
}
</script>

<style lang="scss" scoped>

</style>
