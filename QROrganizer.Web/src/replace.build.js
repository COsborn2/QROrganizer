const replace = require('replace-in-file');
const appInsightsKey = process.argv[2];

const appInsightsKeyOptions = {
    files: 'src/env.ts',
    from: /applicationInsightsKey:.*/gm,
    to: `applicationInsightsKey: '${appInsightsKey}',`,
    allowEmptyPaths: false,
};

try {
    let setAppInsightsKey = replace.sync(appInsightsKeyOptions);
    console.log('App Insights Key:', JSON.stringify(setAppInsightsKey, undefined, 2));
} catch (err) {
    console.log('Err', err);
}
