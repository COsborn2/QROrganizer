<template>
  <v-dialog
    v-model="dialogOn"
    :persistent="isLoading"
    transition="dialog-bottom-transition"
    max-width="400"
  >
    <div class="text-center">
      <v-sheet
        class="px-7 pt-7 pb-4 mx-auto text-center"
        color="blue-grey darken-4"
        dark
      >
        <div class="grey--text text--lighten-1 text-body-2 mb-4">
          {{ confirmationText }}
        </div>

        <v-btn
          :disabled="isLoading"
          class="ma-1"
          color="grey lighten-1"
          plain
          @click="dialogOn = false"
        >
          Cancel
        </v-btn>

        <v-btn
          :loading="isLoading"
          class="ma-1"
          color="error lighten-2"
          plain
          @click="deleteClicked"
        >
          Delete
        </v-btn>
      </v-sheet>
    </div>
  </v-dialog>
</template>

<script lang="ts">
import {Component, Prop, VModel, Vue} from 'vue-property-decorator';

@Component({})
export default class DeleteConfirmationModal extends Vue {
  @VModel({ type: Boolean })
  dialogOn!: boolean;

  @Prop({ type: Boolean, required: true })
  isLoading!: boolean;

  @Prop({ type: String, required: true })
  confirmationText!: string;

  deleteClicked() {
    this.$emit('deleteClicked')
  }
}
</script>

<style lang="scss" scoped>

</style>
