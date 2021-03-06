import Client from '../client';
const resource = '/study';

export default {
    getAll() {
        return Client.get(`${resource}`);
    },
    get(id) {
        return Client.get(`${resource}/${id}`);
    },
    create(payload) {
        return Client.post(`${resource}`, payload);
    },
    update(payload, id) {
        return Client.put(`${resource}/${id}`, payload);
    },
    save(payload, id) {
        return Client.post(`${resource}/${id}`, payload);
    },
    delete(id) {
        return Client.delete(`${resource}/${id}`)
    },
    restore(id) {
        return Client.patch(`${resource}/${id}`)
    },
};
