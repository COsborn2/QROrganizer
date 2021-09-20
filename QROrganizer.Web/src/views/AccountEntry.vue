<template>
  <div class="d-flex justify-center" style="height: 100%; width: 100%">
    <v-container fill-height style="position: fixed; max-width: 800px" class="d-flex align-center justify-center text-center align-center">
      <div style="width: 90%">
        <h1 class="text-uppercase mb-5" style="color: white">{{headerText(this.isInLoginMode)}}</h1>
        <v-form ref="form" v-model="formValid" lazy-validation>
          <v-text-field filled dark color="white" placeholder="Email" type="text" v-model="email" autocomplete="username"/>
          <v-text-field filled dark color="white" placeholder="Password" :type="passwordType"
                        :autocomplete="isInLoginMode ? 'current-password' : 'new-password'"
                        @click:append="showPassword = !showPassword"
                        @focusout="$refs.form.validate()"
                        :append-icon="passwordAppendIcon"
                        v-model="password"
          />
          <v-expand-transition>
            <v-text-field v-if="!isInLoginMode" filled dark color="white" placeholder="Confirm Password" type="text"
                          :autocomplete="isInLoginMode ? '' : 'new-password'"
                          :validate-on-blur="validateOnBlur"
                          @focusout="confirmPasswordFocusOut"
                          v-model="confirmPassword"
                          :rules="[passwordsMatchRule]"
            />
          </v-expand-transition>
        </v-form>

        <v-alert v-if="loginError" border="left" color="error" dark>
          The email/password you entered did not match our records
        </v-alert>

        <v-alert v-if="validationErrors.length > 0" border="left" color="error" dark>
          <ul>
            <li v-for="error in validationErrors">
              {{error}}
            </li>
          </ul>
        </v-alert>

        <v-expand-transition>
          <h4 v-if="!formValid" class="error--text text-bold">Passwords do not match</h4>
        </v-expand-transition>
        <!-- TODO: Add validation from Account Controller in second expand transition -->
        <v-btn class="primary--text" rounded style="width: 100%" @click="submit" :disabled="disableButton">{{headerText(this.isInLoginMode)}}</v-btn>

        <div class="d-flex mt-10 text-center justify-center">
          <h4 style="color: white">{{linkText + '\xa0'}}</h4>
          <h4 @click="changeState" class="linktext">{{headerText(!this.isInLoginMode)}}</h4>
        </div>
      </div>
    </v-container>
  </div>
</template>

<script lang="ts">
import {Component, Prop, Vue} from 'vue-property-decorator';
const axios = require('axios');

@Component({})
export default class AccountEntry extends Vue {
  showPassword: boolean = false;
  isInLoginMode: boolean = false;
  formValid: boolean = true;
  errorText: string = 'Passwords do not match';
  validateOnBlur: boolean = true;
  loginError: boolean = false;
  accountCreateError: boolean = false;
  validationErrors: Array<string> = [];

  email: string = '';
  password: string = '';
  confirmPassword: string = '';

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
    if (this.password.length === 0 || this.confirmPassword.length === 0 || this.isInLoginMode) return true;

    return this.password === this.confirmPassword;
  }

  get passwordType(): string {
    if (!this.isInLoginMode) return 'text';

    return this.showPassword ? 'text' : 'password'
  }

  get passwordAppendIcon(): string {
    if (!this.isInLoginMode) return '';

    return this.showPassword ? 'fas fa-eye' : 'fas fa-eye-slash'
  }

  headerText(currentState: boolean): string {
    return currentState ? 'Login' : 'Sign Up';
  }

  @Prop({ required: true, type: Boolean })
  login!: boolean;

  created() {
    this.isInLoginMode = this.login;

    // Disables mobile scrolling
    document.body.addEventListener('touchmove', this.preventDefault, {passive: false});
  }

  mounted() {
    let element = document.getElementById('vue-app');

    if (!element) return;
    element.setAttribute('style', `background: ${this.$vuetify.theme.currentTheme.primary} !important;`);

    document.body.setAttribute('style', `background: ${this.$vuetify.theme.currentTheme.primary} !important;`);
  }

  preventDefault(e: any) {
    e.preventDefault();
  }

  get disableButton() {
    return (!this.isInLoginMode && this.confirmPassword.length < 1) || this.password.length < 1 || this.email.length < 1 || !this.formValid;
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
      await this.axiosHandler(
          '/login',
          { email: this.email, password: this.password, confirmPassword: '' },
          _ => this.loginError = false);
    } else {
      await this.axiosHandler(
          '/create',
          {email: this.email, password: this.password, confirmPassword: this.confirmPassword},
          (e) => {
            console.log(e);
          });
    }
  }

  async axiosHandler(url: string, data: {email: string, password: string, confirmPassword: string}, errorFunc: (error: any) => void): Promise<boolean> {
    try {
      await axios.post(url, data);
    } catch(e) {
      debugger;
      errorFunc(e);
      return false;
    }

    return true;
  }
}
</script>

<style lang="scss" scoped>
.linktext {
  cursor: pointer;
  color: blue;
}
</style>
