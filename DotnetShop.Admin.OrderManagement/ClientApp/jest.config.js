/** @type {import('ts-jest/dist/types').InitialOptionsTsJest} */
module.exports = {
    preset: "ts-jest",
    testEnvironment: "jsdom",
    moduleNameMapper: {
        "\\.(css|scss)$": "babel-jest",
    },
    setupFilesAfterEnv: ["./jest.setup.tsx"],
};
