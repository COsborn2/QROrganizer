<template>
  <v-tooltip bottom :disabled="!featureDisabled">
    <template v-slot:activator="{ on: onUpper, attrs: attrsUpper}">
      <div v-on="onUpper" v-bind="attrsUpper">
        <v-list-item
          @click="$emit('clicked')"
          :disabled="featureDisabled"
        >
          <v-list-item-icon>
            <v-icon>{{ faIcon }}</v-icon>
          </v-list-item-icon>
          <v-list-item-title>{{ text }}</v-list-item-title>
          <v-tooltip bottom>
            <template v-slot:activator="{ on, attrs }">
              <v-icon
                x-small
                style="position: absolute; top: 5px; right: 5px"
                v-bind="attrs"
                v-on="on"
              >
                fa fa-asterisk
              </v-icon>
            </template>
            <span>Premium Feature</span>
          </v-tooltip>
        </v-list-item>
      </div>
    </template>
    <span>Premium Feature</span>
  </v-tooltip>
</template>

<script lang="ts">
import {Component, Prop, Vue} from 'vue-property-decorator';

@Component({})
export default class PremiumFeatureListItem extends Vue {
  @Prop({ type: String, required: true })
  faIcon!: string;

  @Prop({ type: String, required: true })
  text!: string;

  @Prop({ type: String, required: true })
  requiredFeature!: string;

  get featureDisabled(): boolean {
    return this.$store.getters.activeFeatures.indexOf(this.requiredFeature.toLocaleLowerCase()) === -1;
  }
}
</script>

<style lang="scss" scoped>

</style>
