import Vue from 'vue';
import Vuex from 'vuex';
import { user } from '@/store/context';

Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    user,
  },
});
