const path = require("path");
const { CleanWebpackPlugin } = require("clean-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

module.exports = (env) => {
    const webpack_dev = {
        mode: "development",
        devtool: "cheap-module-source-map",
    };
    const webpack_build = {
        mode: "production",
        devtool: false,
    };
    const webpack_config = env.development ? webpack_dev : webpack_build;

    return {
        ...webpack_config,
        entry: path.resolve(__dirname, "src/index.tsx"),
        resolve: {
            extensions: [".tsx", ".ts", ".js"],
        },
        output: {
            path: path.resolve(__dirname, "dist"),
            filename: "bundle.js",
            publicPath: "/",
        },
        module: {
            rules: [
                {
                    test: /\.(ts|js)x?$/,
                    exclude: /node_modules/,
                    use: [
                        {
                            loader: "babel-loader",
                        },
                    ],
                },
                {
                    test: /\.(c|s[ac])ss$/i,
                    use: [MiniCssExtractPlugin.loader, "css-loader", "sass-loader"],
                },
                {
                    test: /\.(woff(2)|eot|ttf|otf|svg)$/,
                    type: "asset/inline",
                },
            ],
        },
        plugins: [
            ...webpack_config.plugins,
            new CleanWebpackPlugin(),
            new MiniCssExtractPlugin({
                filename: "bundle.css",
            }),
        ],
    };
};
