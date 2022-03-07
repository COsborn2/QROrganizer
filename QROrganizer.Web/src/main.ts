import Vue from 'vue';
import App from './App.vue';
import router, {RouteNames} from './router';
import store from '@/store/index';
import {UserInfoServiceViewModel} from "@/viewmodels.g";

// Import global CSS and Fonts:
import 'typeface-roboto';
import '@fortawesome/fontawesome-free/css/all.css';
import 'vuetify/dist/vuetify.min.css';
import 'coalesce-vue-vuetify/dist/coalesce-vue-vuetify.css';
import '@/site.scss';

// SETUP: vuetify
import Vuetify from 'vuetify';
Vue.use(Vuetify);
const vuetify = new Vuetify({
  icons: {
    iconfont: 'fa', // 'mdi' || 'mdiSvg' || 'md' || 'fa' || 'fa4'
  },
  customProperties: true,
  theme: {
    options: {
      customProperties: true,
    },
    themes: {
      light: {
        primary: '#2fad7b',
        // secondary: "#4d97bc",
        // accent: "#e98f07",
        error: '#df323b', // This is the default error color with darken-1,
        background: '#FFFFFF'
      },
      dark: {
        background: '#121212'
      }
    },
  },
});

// Global defaults for vuetify components. Change as desired.
// @ts-expect-error - No typedefs for vue.options.components
const components: any = Vue.options.components;
components.VInput.options.props.dense.default = true;
components.VTextField.options.props.dense.default = true;
components.VTextField.options.props.outlined.default = true;

// SETUP: coalesce-vue
import { AxiosClient as CoalesceAxiosClient } from 'coalesce-vue';
CoalesceAxiosClient.defaults.baseURL = '/api';
CoalesceAxiosClient.defaults.withCredentials = true;

// SETUP: coalesce-vue-vuetify
import $metadata from '@/metadata.g';
// viewmodels.g has sideeffects - it populates the global lookup on ViewModel and ListViewModel.
import '@/viewmodels.g';
import CoalesceVuetify from 'coalesce-vue-vuetify';
import {UserMutations} from "@/store/UserContext";
import {SiteInfoActions} from "@/store/SiteInfoContext";

Vue.use(CoalesceVuetify, {
  metadata: $metadata,
});

Vue.config.productionTip = false;

let userInfo = new UserInfoServiceViewModel();
userInfo.getUserInfo.setConcurrency('cancel');
userInfo.getUserInfo().then(() => {
  store.commit(UserMutations.SET_ACCOUNT, userInfo.getUserInfo.result);
});

router.beforeEach(async (to, from, next) => {
  // only give users 403 if they are logged in && lack permissions - otherwise go to Login
  if (store.getters.isAuthenticated && to.meta?.roles?.length > 0) {
    if (!(store.getters.roles as string[])
      .some(x => (to.meta?.roles?.map((x: string) => x.toLowerCase()) ?? []).includes(x))) {
      next({ name: RouteNames.Forbidden })
      return;
    }
  }

  if (to.matched.some(record => !record.meta.allowAnonymous)) {
    if (!store.getters.isAuthenticated) {
      next({ name: RouteNames.Login })
      return;
    }
  }

  next();
})

store.dispatch(SiteInfoActions.GET_AND_SET_SITE_INFO).then(() => {
  new Vue({
    store,
    router,
    vuetify,
    render: (h) => h(App),
  }).$mount('#app');
})
