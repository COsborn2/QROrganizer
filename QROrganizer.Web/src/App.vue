<template>
  <v-app id="vue-app">
    <v-navigation-drawer v-if="!hideAppBar" v-model="drawer" class="primary" app clipped overflow>
      <template v-slot:prepend>
        <v-list-item two-line>
          <v-list-item-avatar>
            <v-img src="https://randomuser.me/api/portraits/women/85.jpg"></v-img>
          </v-list-item-avatar>

          <v-list-item-content>
            <v-tooltip right>
              <template v-slot:activator="{ on, attrs }">
                <v-list-item-title v-on="on" class="white--text">cameron0401@gmail.com</v-list-item-title>
              </template>
              <span>cameron0401@gmail.com</span>
            </v-tooltip>
            <v-list-item-subtitle class="white--text">Logged In</v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>

        <v-divider></v-divider>
      </template>

      <v-list>
        <v-list-item v-for="item in Array(15).keys()" :key="item + '1'" link>
          <v-list-item-icon>
            <v-icon style="color: white">fa fa-address-book</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
            <v-list-item-title style="color: white">Address</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>

      <template v-slot:append>
        <div class="pa-2">
          <v-btn class="mb-2" block color="secondary">
            Account Settings
          </v-btn>
          <v-btn block color="error">
            Logout
          </v-btn>
        </div>
      </template>
    </v-navigation-drawer>

    <v-app-bar v-if="!hideAppBar" app color="primary" dark dense clipped-left>
      <!-- TODO: https://vuetifyjs.com/en/components/navigation-drawers/#right -->
      <v-list-item-avatar @click.stop="drawer = !drawer">
        <v-img src="https://randomuser.me/api/portraits/women/85.jpg"></v-img>
      </v-list-item-avatar>
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
import Vue from "vue";
import {Component, Watch} from "vue-property-decorator";

@Component({
  components: {},
})
export default class App extends Vue {
  drawer: boolean | null = null;
  routeComponent: Vue | null = null;

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
    const baseTitle = document.title;
    this.$watch(
      () => (this.routeComponent as any)?.pageTitle,
      (n: string | null | undefined) => {
        if (n) {
          document.title = n + " - " + baseTitle;
        } else {
          document.title = baseTitle;
        }
      },
      { immediate: true }
    );
  }
}
</script>

<style lang="scss">
.router-transition-enter-active,
.router-transition-leave-active {
  // transition: 0.2s cubic-bezier(0.25, 0.8, 0.5, 1);
  transition: 0.1s ease-out;
}

.router-transition-move {
  transition: transform 0.4s;
}

.router-transition-enter,
.router-transition-leave-to {
  opacity: 0;
  // transform: translateY(5px);
}
</style>
