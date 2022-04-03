<template>
  <v-dialog
    v-model="modalEnabled"
    fullscreen
    hide-overlay
    transition="dialog-bottom-transition"
    dark
  >
    <v-card>
      <v-toolbar dark color="primary">
        <v-btn dark icon @click="modalEnabled = false">
          <v-icon dark>fas fa-times</v-icon>
        </v-btn>
        <v-spacer />
        <v-btn rounded color="secondary">
          Review Items
        </v-btn>
      </v-toolbar>

      <div style="position: absolute; left: 50%; top: 50%; width: 100%">
        <div style="width: 80%">
          <timeout-alert
            v-if="previousScanOpen"
            style="position: relative; left: -50%; width: 100%; height: 100%; z-index: 500"
            color="success"
            :text="lastScannedUpcCode"
            :time-out-seconds="2"
            v-on:closed="previousScanOpen = false"
          ></timeout-alert>
        </div>
      </div>

      <StreamBarcodeReader v-if="modalEnabled" @decode="onDecode" ></StreamBarcodeReader>
      <div style="position: absolute; bottom: 5px; width: 100%; height: 25%; overflow-y: scroll; vertical-align: bottom;">
        <div style="position: absolute; bottom: 0; width: 100%; max-height: 100%">
          <UPCScannedItem
            v-for="(item, index) in results"
            :key="item.upcCode"
            v-model="results[index]"
            upc-item-code="887276168630"
            :visible="modalEnabled"
          />
        </div>
      </div>
    </v-card>
  </v-dialog>
</template>

<script lang="ts">
import {Component, Prop, VModel, Vue, Watch} from 'vue-property-decorator';
import {StreamBarcodeReader} from 'vue-barcode-reader';
import TimeoutAlert from "@/components/TimeoutAlert.vue";
import UPCScannedItem from "@/components/UPCScannedItem.vue";
import {ItemViewModel} from "@/viewmodels.g";

@Component({
  components: {UPCScannedItem, StreamBarcodeReader, TimeoutAlert}
})
export default class ItemScanningModal extends Vue {
  @VModel({ type: Boolean })
  modalEnabled!: boolean;

  @Prop({ type: Number, required: true })
  containerId!: number;

  previousScanOpen: boolean = false;
  lastScannedUpcCode: string | null = null;

  results: ItemViewModel[] = [];

  async onDecode(str: any) {
    if (this.previousScanOpen) return;

    let item = new ItemViewModel();
    item.name = item.upcCode = str;
    item.containerId = this.containerId;
    item.quantity = 1;
    item.$load.setConcurrency('debounce');
    item.$save.setConcurrency('debounce');

    item.$removeRule('userId', 'required');

    await item.$save();
    this.results.push(item);

    this.lastScannedUpcCode = str;
    this.previousScanOpen = true;
  }
}
</script>

<style lang="scss" scoped>

</style>
