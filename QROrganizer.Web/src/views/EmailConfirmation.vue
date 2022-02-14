<template>
  <div class="d-flex justify-center primary-background" style="height: 100%; width: 100%">
    <v-container
        fill-height
        style="position: fixed; max-width: 600px"
        class="d-flex align-center justify-center text-center align-center"
    >
      <v-form lazy-validation style="width: 90%">
        <v-expand-transition v-if="userInfoService.confirmEmail.wasSuccessful !== true">
          <div>
            <h1 class="text-uppercase white--text">
              Email Confirmation
            </h1>
            <v-progress-linear class="mb-5" v-if="userInfoService.confirmEmail.isLoading" indeterminate color="secondary" />
            <v-alert v-else-if="userInfoService.confirmEmail.wasSuccessful === false" type="error">
              {{userInfoService.confirmEmail.message}}
            </v-alert>
            <v-text-field
                v-model="userId"
                readonly
                filled
                dark
                color="white"
                placeholder="User Id"
                label="User Id"
                type="text"
                disabled
            />
            <v-text-field
                v-model="confirmationToken"
                readonly
                filled
                dark
                color="white"
                placeholder="Confirmation Token"
                label="Confirmation Token"
                type="text"
                disabled
            />
          </div>
        </v-expand-transition>
        <v-expand-transition v-else>
          <div>
            <v-row>
              <v-col>
                <h1 class="text-uppercase white--text">
                  Email Confirmed!
                </h1>
              </v-col>
            </v-row>
            <v-row>
              <v-col>
                <v-btn
                    class="primary--text"
                    rounded
                    style="width: 100%"
                    @click="buttonClicked"
                >
                  Continue to Login
                </v-btn>
              </v-col>
            </v-row>
          </div>
        </v-expand-transition>
      </v-form>
    </v-container>
  </div>
</template>

<script lang="ts">
import {Component, Vue} from 'vue-property-decorator';
import {UserInfoServiceViewModel} from "@/viewmodels.g";
import {RouteNames} from "@/router";

@Component({})
export default class EmailConfirmation extends Vue {
  confirmationToken: string | null = null;
  userId: string | null = null;

  userInfoService = new UserInfoServiceViewModel();

  buttonClicked() {
    this.$router.push({ name: RouteNames.Login });
  }

  mounted() {
    if (this.$route.query?.confirmationToken) {
      this.confirmationToken = (this.$route.query.confirmationToken as string);
    }
    if (this.$route.query?.userId) {
      this.userId = (this.$route.query.userId as string);
    }
    this.userInfoService.confirmEmail.invoke(this.userId, this.confirmationToken);
  }
}
</script>

<style lang="scss" scoped>

</style>
