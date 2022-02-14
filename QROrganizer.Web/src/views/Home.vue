<template>
  <div class="home">
    <HelloWorld :msg="message" />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import HelloWorld from '@/components/HelloWorld.vue'; // @ is an alias to /src

@Component({
  components: {
    HelloWorld,
  },
})
export default class Home extends Vue {
  mounted() {
  }

  get message(): string {
    return this.$store.state.user.email;
  }
}
</script>
