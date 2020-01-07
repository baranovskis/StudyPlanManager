import axios from "axios";

const baseDomain = "http://localhost:9000/api";
const baseURL = `${baseDomain}`; // Incase of /api/v1;

export default axios.create({
  baseURL
});
