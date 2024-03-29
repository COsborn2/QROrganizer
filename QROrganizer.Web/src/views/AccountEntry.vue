<template>
  <div class="d-flex justify-center primary-background" style="height: 100%; width: 100%">
    <v-container
      fill-height
      style="position: fixed; max-width: 600px"
      class="d-flex align-center justify-center text-center align-center"
    >
      <div v-if="!success" style="width: 90%">
        <template v-if="requireAccessCode">
          <h1 class="text-uppercase white--text">
            Restricted Environment
          </h1>
          <h2 class="font-weight-thin white--text mb-5">
            Enter access code to continue
          </h2>
          <access-code-entry v-model="accessCode" v-on:continue="accessCodeEntered = true" />
        </template>
        <template v-else>
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
                label="Email"
                type="text"
                autocomplete="username"
            />
            <v-expand-transition>
              <v-text-field
                  v-if="!isInLoginMode"
                  v-model="username"
                  maxlength="16"
                  counter="16"
                  filled
                  dark
                  color="white"
                  placeholder="Username"
                  label="Username"
                  type="text"
                  autocomplete="username"
                  :rules="[x => !!x, x => x.length <= 16]"
                  @focusout
              />
            </v-expand-transition>
            <v-text-field
                ref="passwordField"
                v-model="password"
                filled
                dark
                color="white"
                placeholder="Password"
                label="Password"
                :type="passwordType"
                :autocomplete="isInLoginMode ? 'current-password' : 'new-password'"
                :validate-on-blur="validateOnBlur"
                :append-icon="passwordAppendIcon"
                :rules="[passwordsMatchRule(false), password.length > 1]"
                @click:append="showPassword = !showPassword"
                @focusout="focusOut"
                @keydown.enter="submit"
            />
            <v-expand-transition v-if="!isInLoginMode">
              <div>
                <v-text-field
                    ref="confirmPasswordField"
                    v-model="confirmPassword"
                    filled
                    dark
                    color="white"
                    placeholder="Confirm Password"
                    label="Confirm Password"
                    type="text"
                    :autocomplete="isInLoginMode ? '' : 'new-password'"
                    :validate-on-blur="validateOnBlur"
                    :rules="[passwordsMatchRule(true), confirmPassword.length > 1]"
                    @focusout="confirmPasswordFocusOut"
                />
                <v-checkbox dark v-model="emailConsentCheckbox" :rules=[this.emailConsentCheckbox]>
                  <template v-slot:label>
                    <h4 class="white--text">
                      I understand my email will be used for account verification and recovery as well as subscription
                      confirmation and billing changes
                    </h4>
                  </template>
                </v-checkbox>

                <vue-hcaptcha ref="captcha" sitekey="41749397-9529-40b7-81f2-38faeec88de0" v-on:verify="captchaVerify"></vue-hcaptcha>
              </div>
            </v-expand-transition>
          </v-form>
        </template>

        <v-expand-transition>
          <v-alert
              v-if="validationErrors.length > 0"
              border="left"
              type="error"
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
        </v-expand-transition>

        <v-alert v-if="showPasswordsDoNotMatchPrompt" type="error" border="left" dark>
          Passwords do not match
        </v-alert>

        <v-btn
          v-if="!requireAccessCode"
          class="primary--text"
          rounded
          style="width: 100%"
          :disabled="disableButton && !isLoading"
          :loading="isLoading"
          @click="submit"
          @keydown.enter.native="submit"
        >
          {{ headerText(isInLoginMode) }}
        </v-btn>

        <div class="d-flex mt-10 text-center justify-center">
          <h4 style="color: white">{{ linkText + '\xa0' }}</h4>
          <h4 class="linktext" @click="changeState">
            {{ headerText(!isInLoginMode) }}
          </h4>
        </div>
      </div>
      <div v-else>
        <v-icon style="font-size: 100px">fas fa-check-circle</v-icon>
        <h2 class="white--text">
          We went you a confirmation email. Check your email to continue.
        </h2>
      </div>
    </v-container>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { AxiosClient } from "coalesce-vue/lib/api-client";
import { UserMutations } from "@/store/UserContext";
import AccessCodeEntry from "@/components/AccessCodeEntry.vue";
import VueHcaptcha from "@hcaptcha/vue-hcaptcha";
import {AxiosRequestConfig} from "axios";
import {RouteNames} from "@/router";

export class LoginCredentials {
  username: string | null = null;
  email: string | null = null;
  password: string | null = null;
  confirmPassword: string | null = null;
  restrictedAccessCode: string | null = null;
}

@Component({
  components: {
    AccessCodeEntry,
    VueHcaptcha
  }
})
export default class AccountEntry extends Vue {
  showPassword = false;
  isInLoginMode = false;
  formValid = false;
  errorText = 'Passwords do not match';
  validateOnBlur = true;
  accountCreateError = false;
  validationErrors: Array<string> = [];
  isLoading = false;
  success = false;
  client = AxiosClient;
  accessCodeEntered = false;
  accessCode = '';

  email = '';
  username = '';
  password = '';
  confirmPassword = '';
  emailConsentCheckbox = false;

  captchaToken: string | null = null;

  captchaVerify(token: string) {
    this.captchaToken = token;
  }

  get requireAccessCode() {
    return this.$store.getters.isRestrictedEnvironment && !this.isInLoginMode && !this.accessCodeEntered;
  }

  focusOut() {
    if (this.confirmPassword.length < 1 || (this.$refs.confirmPasswordField as any).isFocused) return;
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
    this.validationErrors = [];
    this.isInLoginMode = !this.isInLoginMode;
    this.emailConsentCheckbox = false;

    this.validateOnBlur = true;
    this.confirmPassword = '';
  }

  get showPasswordsDoNotMatchPrompt() {
    if (this.someMissingLength(this.password, this.confirmPassword)) return false;

    return !this.formValid && (this.password !== this.confirmPassword);
  }

  passwordsMatchRule(confirmPasswordPrompt: boolean = false) {
    if (!confirmPasswordPrompt
        && (this.$refs.confirmPasswordField as any)?.isFocused === true) {
      return true;
    }

    if (
      this.password.length === 0 ||
      this.confirmPassword.length === 0 ||
      this.isInLoginMode
    ) {
      return true;
    }

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

  someMissingLength(...toEvaluate: string[]) {
    return toEvaluate.some(x => x.length < 1);
  }
  
  get isDevMode() {
    return Vue.config.devtools;
  }

  get disableButton() {
    if (this.isInLoginMode) {
      return this.someMissingLength(this.email, this.password) || !this.formValid;
    }

    return this.someMissingLength(this.email, this.username, this.password, this.confirmPassword)
        || this.password !== this.confirmPassword
        || !this.formValid
        || (!this.isDevMode && !this.captchaToken);
  }

  async submit() {
    this.validateOnBlur = false;
    (this.$refs.form as any).validate();
    if (!this.formValid) return;

    this.validationErrors = [];

    let loginCredentials = new LoginCredentials();
    loginCredentials.email = this.email;
    loginCredentials.password = this.password;

    if (this.isInLoginMode) {
      let res = await this.axiosHandler(
        '/login',
        loginCredentials,
        (e) => {
          if ((e?.response?.data?.errors?.length ?? 0) > 0) {
            this.validationErrors = e.response.data.errors;
          } else {
            this.validationErrors = ['Login failed - check email/password'];
          }

          return;
        },
      );

      if (res[0]) {
        this.$store.commit(UserMutations.SET_ACCOUNT, res[1])
        this.$router.push({name: RouteNames.Home});
      }
    } else {
      loginCredentials.username = this.username;
      loginCredentials.confirmPassword = this.confirmPassword;

      if (this.accessCodeEntered) {
        loginCredentials.restrictedAccessCode = this.accessCode;
      }

      let res = await this.axiosHandler(
        '/create',
        loginCredentials,
        (e) => {
          let errors: { code: string; description: string }[] =
            e.response.data.errors;
          if (errors?.length > 1) {
            this.validationErrors = errors.map((x) => x.description);
          } else if (e.response.data){
            this.validationErrors = [e.response.data];
          } else {
            this.validationErrors = ['An error occurred'];
          }
          (this.$refs.captcha as any).reset();
        },
          {headers: {
            'h-captcha-response': this.captchaToken
          }}
      );
      this.success = res[0];
    }
  }

  async axiosHandler(
    url: string,
    data: LoginCredentials,
    errorFunc: (error: any) => void,
    config: AxiosRequestConfig | null = null,
  ): Promise<[boolean, LoginCredentials | null]> {
    this.isLoading = true;
    try {
      let res = await this.client.post<LoginCredentials>(url, data, config ? config : {});
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
</style>
