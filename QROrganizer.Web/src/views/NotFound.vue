<template>
  <v-container fill-height>
    <v-row>
      <v-col>
        <h1 class="text-center display-4 font-weight-bold">
          404
        </h1>
        <h2 class="text-center display-2 font-weight-light">
          Page Not Found
        </h2>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <h1 class="text-center display-2 font-weight-thin">Ruh Roh</h1>
      </v-col>
    </v-row>
  </v-container>
</template>
<script lang="ts">
import {Component, Prop, Vue} from 'vue-property-decorator';

@Component({})
export default class NotFound extends Vue {

}
</script>

<style lang="scss" scoped>

</style>
