import Vue from 'vue';
import Router from 'vue-router';

import Home from './views/Home.vue';
import About from './views/About.vue';
import AccountEntry from '@/views/AccountEntry.vue';
import Forbidden from "@/views/Forbidden.vue";
import NotFound from "@/views/NotFound.vue";

Vue.use(Router);

export enum RouteNames {
  Home = "Home",
  Login = "Login",
  SignUp = "Sign Up",
  Forbidden = "Forbidden",
  NotFound = "404"
}

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: RouteNames.Home,
      component: Home,
    },
    {
      path: '/about',
      name: 'about',
      component: About,
      meta: {
        roles: ['Admin']
      }
    },
    {
      path: '/login',
      name: RouteNames.Login,
      component: AccountEntry,
      meta: {
        hideAppBar: true,
        allowAnonymous: true,
      },
      props: () => ({
        login: true,
      }),
    },
    {
      path: '/signup',
      name: RouteNames.SignUp,
      component: AccountEntry,
      meta: {
        hideAppBar: true,
        allowAnonymous: true,
      },
      props: () => ({
        login: false,
      }),
    },
    {
      path: '/forbidden',
      name: RouteNames.Forbidden,
      component: Forbidden,
    },


    {
      path: '*',
      name: RouteNames.NotFound,
      component: NotFound,
    }

    // // Coalesce admin routes
    // {
    //   path: '/admin/:type',
    //   name: 'coalesce-admin-list',
    //   component: CAdminTablePage,
    //   props: (r) => ({
    //     type: r.params.type,
    //   }),
    // },
    // {
    //   path: '/admin/:type/edit/:id?',
    //   name: 'coalesce-admin-item',
    //   component: CAdminEditorPage,
    //   props: (r) => ({
    //     type: r.params.type,
    //     id: r.params.id,
    //   }),
    // },
  ],
});
