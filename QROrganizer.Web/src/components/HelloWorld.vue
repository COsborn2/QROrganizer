<template>
  <v-container>
    <h1>
      {{ msg }}
    </h1>
    <v-row> </v-row>
  </v-container>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component
export default class HelloWorld extends Vue {
  @Prop() private msg!: string;

  // async create() {
  //   let res = await axios.post('/create', {Email: 'email', Password: 'password'})
  // }
  //
  // async login() {
  //   let res = await axios.post('/login', {Email: 'email', Password: 'password'})
  // }
  //
  // async requestInfo() {
  //   let res = await this.userService.userInfo.invoke();
  //   console.log(res.data.object?.roles)
  //   console.log(res.data.object?.userName)
  // }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss"></style>
