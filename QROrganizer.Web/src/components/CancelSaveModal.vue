<template>
  <v-dialog
    @keydown.enter="saveClicked"
    v-model="dialogOn"
    :persistent="isLoading"
    transition="dialog-bottom-transition"
    max-width="600"
  >
    <template v-slot:default="dialog" v-if="dialogOn">
      <v-card>
        <v-toolbar color="primary" dark>
          {{ headerText }}
        </v-toolbar>
        <v-progress-linear indeterminate v-if="isLoading" color="secondary" />
        <v-card-text>
          <slot></slot>
        </v-card-text>
        <v-card-actions class="justify-end">
          <v-btn
            text
            @click="dialog.value = false"
            :disabled="isLoading"
          >Close</v-btn>
          <v-btn :disabled="isLoading" color="success" @click="saveClicked">Save</v-btn>
        </v-card-actions>
      </v-card>
    </template>
  </v-dialog>
</template>

<script lang="ts">
import {Component, Prop, VModel, Vue} from 'vue-property-decorator';

@Component({})
export default class CancelSaveModal extends Vue {
  @Prop({ type: String, required: true })
  headerText!: string;

  @Prop({ type: Boolean, required: true })
  isLoading!: boolean;

  @VModel({ type: Boolean })
  dialogOn!: boolean;

  saveClicked() {
    this.$emit('saveClicked');
  }
}
</script>

<style lang="scss" scoped>

</style>
