import Client from '../client';
const resource = '/settings';

export default {
    get() {
        return Client.get(`${resource}`);
    },
    save(payload) {
        return Client.post(`${resource}`, payload);
    },
    getProject(id) {
        return Client.get(`${resource}/${id}`);
    },
    saveProject(payload, id) {
        return Client.post(`${resource}/${id}`, payload);
    },
};
