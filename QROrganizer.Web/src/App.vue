<template>
  <v-app id="vue-app">
    <v-navigation-drawer
      v-if="!hideAppBar"
      v-model="drawer"
      clipped
      app
      overflow
    >
      <template v-slot:prepend>
        <v-list-item two-line>
          <v-list-item-avatar>
            <v-icon x-large>fas fa-user</v-icon>
          </v-list-item-avatar>

          <v-list-item-content>
            <v-list-item-title>{{ $store.state.user.username }}</v-list-item-title>
            <v-list-item-subtitle> Logged In </v-list-item-subtitle>
          </v-list-item-content>

        </v-list-item>

        <div class="pa-2">
          <v-btn class="mb-2" block color="secondary" disabled> Account Settings </v-btn>
          <v-btn block color="error" @click="logout" :loading="logoutLoading"> Logout </v-btn>
        </div>

        <v-divider></v-divider>
      </template>

      <v-list class="mb-16">
        <v-list-item link to="/">
          <v-list-item-icon>
            <v-icon>fa-solid fa-box</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
            <v-list-item-title>Containers</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>

      <v-spacer></v-spacer>

      <div class="site-info-footer text-center font-weight-thin theme-background-color">
        <v-divider />
        <span>Build Date: {{formattedBuildDate}}</span>
      </div>
    </v-navigation-drawer>

    <v-app-bar v-if="!hideAppBar" app color="primary" dark dense clipped-left>
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>
      <v-toolbar-title>
        <router-link to="/" class="white--text" style="text-decoration: none">
          Box Organizer
        </router-link>
      </v-toolbar-title>
    </v-app-bar>

    <v-main>
      <transition
        name="router-transition"
        mode="out-in"
        appear
        @enter="routerViewOnEnter"
      >
        <!-- https://stackoverflow.com/questions/52847979/what-is-router-view-key-route-fullpath -->
        <router-view ref="routerView" :key="$route.path" />
      </transition>
    </v-main>
  </v-app>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import {AxiosInstance} from "axios";
import {AxiosClient} from "coalesce-vue/lib/api-client";
import {RouteNames} from "@/router";
import {UserActions} from "@/store/UserContext";
import moment from "moment";

@Component({
  components: {},
})
export default class App extends Vue {
  drawer: boolean = false;
  routeComponent: Vue | null = null;
  client: AxiosInstance = AxiosClient;
  logoutLoading = false;

  get formattedBuildDate() {
    return moment(this.$store.state.siteInfo.buildDate).format('MM/DD/YYYY hh:mm')
  }

  async logout() {
    try {
      this.logoutLoading = true;
      await this.$store.dispatch(UserActions.LOGOUT);
    } finally {
      this.logoutLoading = false;
    }

    this.$router.push({name: RouteNames.Login})
  }

  get hideAppBar() {
    return this.routeMeta?.hideAppBar === true;
  }

  get routeMeta() {
    if (!this.$route || this.$route.name === null) return null;

    return this.$route.meta;
  }

  routerViewOnEnter() {
    this.routeComponent = this.$refs.routerView as Vue;
  }

  created() {
    this.drawer = false;
    const baseTitle = document.title;
    this.$watch(
      () => (this.routeComponent as any)?.pageTitle,
      (n: string | null | undefined) => {
        if (n) {
          document.title = n + ' - ' + baseTitle;
        } else {
          document.title = baseTitle;
        }
      },
      { immediate: true },
    );
  }
}
</script>

<style lang="scss">
.site-info-footer {
  position: absolute;
  bottom: 0;
  padding: 10px;
}
.router-transition-enter-active,
.router-transition-leave-active {
  transition: 0.1s ease-out;
}

.router-transition-move {
  transition: transform 0.4s;
}

.router-transition-enter,
.router-transition-leave-to {
  opacity: 0;
}
</style>
