import { BEARER_TOKEN } from "../Constants";

export const authHeader = () => {
    const token = localStorage.getItem(BEARER_TOKEN);
    if (token) {
        return { Authorization: `Bearer ${token}` };
    } else {
        return { Authorization: "" };
    }
};

export const isTokenExpired = (token: string) => {
    if (token) {
        const expiry = JSON.parse(window.atob(token.split(".")[1])).exp;
        return Math.floor(new Date().getTime() / 1000) >= expiry;
    } else {
        return true;
    }
};
