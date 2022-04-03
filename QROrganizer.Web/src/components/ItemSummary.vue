<template>
  <v-sheet class="mb-2" rounded :color="wasSuccessful ? 'success' : 'error'" style="padding: 0 1rem">
    <div class="mb-2">
      <v-row class="mt-2">
        <v-col class="text-center">
          <h2 class="font-weight-bold">
            {{ item.name }}
          </h2>
        </v-col>
      </v-row>
      <v-row class="mt-0">
        <v-col>
          <h4>Quantity</h4>
        </v-col>
        <v-col>
          <h4>{{ item.quantity }}</h4>
        </v-col>
      </v-row>
      <v-row class="mt-0" v-if="item.itemBarcodeInformation">
        <v-col>
          <h4>Upc Code</h4>
        </v-col>
        <v-col>
          <h4>{{ item.itemBarcodeInformation.upcCode }}</h4>
        </v-col>
      </v-row>
      <v-row class="mt-0" v-if="!wasSuccessful">
        <v-col class="text-center">
          <h3>
            <v-icon>fas fa-exclamation-triangle</v-icon>
            {{ !item.itemBarcodeInformation ? 'Item failed to search' : 'UPC code could not be found' }}
          </h3>
        </v-col>
      </v-row>
    </div>
  </v-sheet>
</template>

<script lang="ts">
import {Component, Prop, VModel, Vue} from 'vue-property-decorator';
import {ItemViewModel} from "@/viewmodels.g";

@Component({})
export default class ItemSummary extends Vue {
  @VModel({ type: ItemViewModel, required: true })
  item!: ItemViewModel;

  get wasSuccessful(): boolean {
    return this.item.itemBarcodeInformation?.wasSuccessful === true;
  }
}
</script>

<style lang="scss" scoped>

</style>
