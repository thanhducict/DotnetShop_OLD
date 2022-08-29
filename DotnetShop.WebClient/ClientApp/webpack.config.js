const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const { CleanWebpackPlugin } = require("clean-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

module.exports = (env) => {
    const webpack_server = {
        mode: "development",
        devtool: "cheap-module-source-map",
        plugins: [new HtmlWebpackPlugin({ template: path.resolve(__dirname, "src/index.html") })],
        devServer: {
            historyApiFallback: true,
        },
    };
    const webpack_dev = {
        mode: "development",
        devtool: "cheap-module-source-map",
        plugins: [],
    };
    const webpack_build = {
        mode: "production",
        devtool: false,
        plugins: [],
    };
    const webpack_config = env.server ? webpack_server : env.development ? webpack_dev : webpack_build;

    return {
        ...webpack_config,
        entry: path.resolve(__dirname, "src/index.tsx"),
        resolve: {
            extensions: [".tsx", ".ts", ".js"],
        },
        output: {
            path: path.resolve(__dirname, "dist"),
            filename: "webclient.bundle.js",
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
                filename: "webclient.bundle.css",
            }),
        ],
    };
};
