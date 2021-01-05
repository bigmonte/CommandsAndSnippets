export default {
    data(){
        return {
            alert: this.initializeAlert(),
            timeOutId: null
        }
    },
    methods: {
        initializeAlert(){
            return {success: null, error: null}
        },
        clearAlertTimeout(){
            this.timeOutId && clearTimeout(this.timeOutId)
        },
        setAlert(type, message){
            this.alert = this.initializeAlert()
            this.alert[type] = message
            this.timeOutId = setTimeout(() => this.alert = this.initializeAlert(), 2000)
        }
    
    
    }
}