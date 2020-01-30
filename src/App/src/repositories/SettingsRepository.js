import Client from '../client';
const resource = '/settings';

export default {
    get(id) {
        return Client.get(`${resource}/${id}`);
    },
    save(payload, id) {
        return Client.post(`${resource}/${id}`, payload);
    },
};
