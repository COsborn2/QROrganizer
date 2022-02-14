<template>
  <div>
    <v-form lazy-validation>
      <v-text-field
          v-model="accessCode"
          filled
          dark
          color="white"
          placeholder="Access Code"
          label="Access Code"
          type="text"
          :loading="accessCodeService.isAccessCodeValid.isLoading"
          :rules="[() => !isAccessCodeValid]"
      />
      <v-alert v-if="!isAccessCodeValid && accessCodeService.isAccessCodeValid.result !== null" type="error" border="left" dark>
        Access code invalid
      </v-alert>
      <v-btn
          class="primary--text"
          rounded
          style="width: 100%"
          :disabled="isButtonDisabled"
          :loading="accessCodeService.isAccessCodeValid.isLoading"
          @click="submit"
          @keydown.enter.native="submit"
      >
        Continue
      </v-btn>
    </v-form>
  </div>
</template>

<script lang="ts">
import {Component, VModel, Vue, Watch} from 'vue-property-decorator';
import {AccessCodeServiceViewModel} from "@/viewmodels.g";
import _ from 'lodash';

@Component({})
export default class AccessCodeEntry extends Vue {
  accessCodeService = new AccessCodeServiceViewModel();

  searchFunc!: () => void;

  get errorMessage(): string | [] {
    return this.isAccessCodeValid || this.accessCodeService.isAccessCodeValid === null ? [] : 'Access code invalid';
  }

  mounted() {
    if (this.$route.query?.accessCode) {
      this.accessCode = (this.$route.query.accessCode as string);
    }

    this.searchFunc = _.debounce(() => {
      if (this.accessCode?.length > 0) {
        this.accessCodeService.isAccessCodeValid.invoke(this.accessCode);
      }
    }, 100);
  }

  @VModel({ type: String })
  accessCode!: string;

  @Watch('accessCode')
  watchValue() {
    console.log('here');
    this.searchFunc();

    this.$emit('input', this.accessCode);
  }

  get isAccessCodeValid() {
    return this.accessCodeService.isAccessCodeValid?.result === true;
  }

  get isButtonDisabled() {
    return this.accessCodeService.isAccessCodeValid.isLoading || !this.isAccessCodeValid;
  }

  submit() {
    this.$emit('continue');
  }
}
</script>

<style lang="scss" scoped>

</style>
