<template>
  <v-dialog
    v-model="modalEnabled"
    fullscreen
    hide-overlay
    transition="dialog-bottom-transition"
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

      <StreamBarcodeReader @decode="onDecode" ></StreamBarcodeReader>
      <span v-for="(item, i) in results" :key="item + 'i' + i">Code - {{ item }}</span>
    </v-card>
  </v-dialog>
</template>

<script lang="ts">
import {Component, VModel, Vue, Watch} from 'vue-property-decorator';
import {StreamBarcodeReader} from 'vue-barcode-reader';

@Component({
  components: {StreamBarcodeReader}
})
export default class ItemScanningModal extends Vue {
  @VModel({ type: Boolean })
  modalEnabled!: boolean;

  items: MediaDeviceInfo[] = [];
  selectedCamera: string | null = null;
  results: string[] = [];

  onDecode(str: any) {
    this.results.push(str);
  }
}
</script>

<style lang="scss" scoped>

</style>
