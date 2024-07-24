// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

const baseServiceUrl = 'http://localhost:5074/api';
const services = {
    auth: baseServiceUrl + '/auth',
    repositories: baseServiceUrl + '/repositories',
    bookmarks: baseServiceUrl + '/bookmarks',
};

export const environment = {
    production: false,
    services
};
