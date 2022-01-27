<template>
  <div class="d-flex justify-center primary-background" style="height: 100%; width: 100%">
    <v-dialog v-model="success" width="500" fullscreen>
      <div
        class="d-flex align-center flex-column justify-center text-center align-center"
        style="width: 100%; height: 100%; backdrop-filter: blur(10px)"
      >
        <v-icon style="font-size: 120px">fas fa-5x fa-check-circle</v-icon>
        <h1>Redirecting...</h1>
      </div>
    </v-dialog>
    <v-container
      fill-height
      style="position: fixed; max-width: 800px"
      class="d-flex align-center justify-center text-center align-center"
    >
      <div style="width: 90%">
        <h1 class="text-uppercase mb-5" style="color: white">
          {{ headerText(isInLoginMode) }}
        </h1>
        <v-form ref="form" v-model="formValid" lazy-validation>
          <v-text-field
            v-model="email"
            filled
            dark
            color="white"
            placeholder="Email"
            type="text"
            autocomplete="username"
          />
          <v-text-field
            v-model="password"
            filled
            dark
            color="white"
            placeholder="Password"
            :type="passwordType"
            :autocomplete="isInLoginMode ? 'current-password' : 'new-password'"
            :append-icon="passwordAppendIcon"
            @click:append="showPassword = !showPassword"
            @focusout="focusOut"
            @keydown.enter="submit"
          />
          <v-expand-transition>
            <v-text-field
              v-if="!isInLoginMode"
              v-model="confirmPassword"
              filled
              dark
              color="white"
              placeholder="Confirm Password"
              type="text"
              :autocomplete="isInLoginMode ? '' : 'new-password'"
              :validate-on-blur="validateOnBlur"
              :rules="[passwordsMatchRule]"
              @focusout="confirmPasswordFocusOut"
            />
          </v-expand-transition>
        </v-form>

        <v-alert v-if="loginError" border="left" color="error" dark>
          The email/password you entered did not match our records
        </v-alert>

        <v-alert
          v-if="validationErrors.length > 0"
          border="left"
          color="error"
          dark
        >
          <ul>
            <li
              v-for="error in validationErrors"
              :key="error"
              style="text-align: start"
            >
              {{ error }}
            </li>
          </ul>
        </v-alert>

        <v-alert v-if="!formValid" color="error" border="left" dark
          >Passwords do not match</v-alert
        >

        <!-- TODO: Add validation from Account Controller in second expand transition -->
        <v-btn
          class="primary--text"
          rounded
          style="width: 100%"
          :disabled="disableButton && !isLoading"
          :loading="isLoading"
          @click="submit"
          @keydown.enter.native="submit"
        >
          <!--          <div v-if="!success">-->
          {{ headerText(isInLoginMode) }}
          <!--          </div>-->
          <!--          <v-icon v-else>fas fa-check-circle</v-icon>-->
        </v-btn>

        <div class="d-flex mt-10 text-center justify-center">
          <h4 style="color: white">{{ linkText + '\xa0' }}</h4>
          <h4 class="linktext" @click="changeState">
            {{ headerText(!isInLoginMode) }}
          </h4>
        </div>
      </div>
    </v-container>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import axios from "axios";
import {UserMutations} from "@/store/context";
import {UserState} from "@/store/UserState";

@Component({})
export default class AccountEntry extends Vue {
  showPassword = false;
  isInLoginMode = false;
  formValid = true;
  errorText = 'Passwords do not match';
  validateOnBlur = true;
  loginError = false;
  accountCreateError = false;
  validationErrors: Array<string> = [];
  isLoading = false;
  success = false;

  email = '';
  password = '';
  confirmPassword = '';

  focusOut() {
    if (!(this.$refs?.form as any)?.validate) return;
    (this.$refs.form as any).validate();
  }

  get linkText(): string {
    return this.isInLoginMode ? 'New User?' : 'Returning User?';
  }

  confirmPasswordFocusOut() {
    (this.$refs.form as any).validate();
    this.validateOnBlur = false;
  }

  changeState() {
    this.isInLoginMode = !this.isInLoginMode;

    this.validateOnBlur = true;
    this.confirmPassword = '';

    (this.$refs.form as any).validate();
  }

  passwordsMatchRule() {
    if (
      this.password.length === 0 ||
      this.confirmPassword.length === 0 ||
      this.isInLoginMode
    )
      return true;

    return this.password === this.confirmPassword;
  }

  get passwordType(): string {
    if (!this.isInLoginMode) return 'text';

    return this.showPassword ? 'text' : 'password';
  }

  get passwordAppendIcon(): string {
    if (!this.isInLoginMode) return '';

    return this.showPassword ? 'fas fa-eye' : 'fas fa-eye-slash';
  }

  headerText(currentState: boolean): string {
    return currentState ? 'Login' : 'Sign Up';
  }

  @Prop({ required: true, type: Boolean })
  login!: boolean;

  created() {
    this.isInLoginMode = this.login;
  }

  preventDefault(e: any) {
    e.preventDefault();
  }

  get disableButton() {
    return (
      (!this.isInLoginMode && this.confirmPassword.length < 1) ||
      this.password.length < 1 ||
      this.email.length < 1 ||
      !this.formValid
    );
  }

  async submit() {
    this.validateOnBlur = false;
    (this.$refs.form as any).validate();
    if (!this.formValid) return;

    // TODO: Remove this - for testing
    console.log(this.email);
    console.log(this.password);
    console.log(this.confirmPassword);

    this.loginError = false;
    this.validationErrors = [];

    if (this.isInLoginMode) {
      let res = await this.axiosHandler(
        '/login',
        { email: this.email, password: this.password, confirmPassword: '' },
        () => (this.loginError = true),
      );
      this.success = res[0];

      if (res[0]) {
        this.$store.commit(UserMutations.SET_ACCOUNT, res[1])
      }
    } else {
      let res = await this.axiosHandler(
        '/create',
        {
          email: this.email,
          password: this.password,
          confirmPassword: this.confirmPassword,
        },
        (e) => {
          let errors: { code: string; description: string }[] =
            e.response.data.errors;
          this.validationErrors = errors.map((x) => x.description);
        },
      );

      this.success = res[0];
    }

    if (this.success){
      this.$router.push({name: 'home'});
    }
  }

  async axiosHandler(
    url: string,
    data: { email: string; password: string; confirmPassword: string },
    errorFunc: (error: any) => void,
  ): Promise<[boolean, UserState | null]> {
    this.isLoading = true;
    try {
      let res = await axios.post<UserState>(url, data);
      this.isLoading = false;
      return [true, res.data];
    } catch (e) {
      errorFunc(e);
      this.isLoading = false;
      return [false, null];
    }
  }
}
</script>

<style lang="scss" scoped>
.linktext {
  cursor: pointer;
  color: blue;
}
.primary-background {
  background-color: var(--v-primary-base) !important;
}
</style>
