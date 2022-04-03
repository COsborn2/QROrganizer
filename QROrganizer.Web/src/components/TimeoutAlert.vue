<template>
  <v-alert v-if="open" :color="color" dense>
    <v-row align="center" no-gutters>
      <v-col class="shrink mr-2">
        <v-progress-circular :rotate="-90" :value="percent" color="accent" />
      </v-col>
      <v-col class="grow">
        {{ text }}
      </v-col>
    </v-row>
  </v-alert>
</template>

<script lang="ts">
import {Component, Prop, VModel, Vue, Watch} from 'vue-property-decorator';

@Component({})
export default class TimeoutAlert extends Vue {
  @Prop({ type: String, required: true })
  color!: string;

  @Prop({ type: String, required: true })
  text!: string;

  @Prop({ type: Number, required: true })
  timeOutSeconds!: number;

  timeRemainingMS: number = 0;
  interval: any;

  get percent(): number {
    return ((this.timeRemainingMS) / (this.timeOutSeconds * 1000)) * 100;
  }

  @Watch('open')
  dispose() {
    if (!this.open) {
      this.timeRemainingMS = 0;
      clearInterval(this.interval);
    }
  }

  open: boolean = true;
  intervalDelay = 500;

  mounted() {
    this.timeRemainingMS = this.timeOutSeconds * 1000;

    this.interval = setInterval(() => {
      if (this.timeRemainingMS < 0) {
        this.open = false;
        this.$emit('closed');
        clearInterval(this.interval);
        return;
      }

      this.timeRemainingMS -= this.intervalDelay;
    }, this.intervalDelay);
  }
}
</script>

<style lang="scss" scoped>

</style>
