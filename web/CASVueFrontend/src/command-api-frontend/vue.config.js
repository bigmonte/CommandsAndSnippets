module.exports = {
    devServer: {
        https: true,
        proxy: {
            '^/api' : {
                target: 'http://localhost:5000'
            }
        }
    }
}