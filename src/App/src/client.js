import axios from "axios";

const baseDomain = "http://localhost:9000/api";
const baseURL = `${baseDomain}`;

export default axios.create({
  baseURL
});
