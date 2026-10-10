

import axios from "axios";

export default function backendApi(token: string) {
  return axios.create({
    baseURL: import.meta.env.VITE_API_URL,
    headers: {    
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  });
}


