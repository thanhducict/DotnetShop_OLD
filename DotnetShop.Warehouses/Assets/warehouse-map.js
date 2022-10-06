export class WarehouseMap {
    init() {
        if (document.querySelector(".warehouse-container__warehouse-map").length === 0) {
            return;
        }

        let instance = this;
        instance.loadScript("https://www.bing.com/api/maps/mapcontrol?&callback=getMap");
        window.getMap = () => {
            instance.loadMapScenario();
        }
    }

    loadScript(url) {
        let script = document.createElement("script");
        script.type = "text/javascript";
        script.async = true;
        script.defer = true;
        script.src = url;
        document.getElementsByTagName("head")[0].appendChild(script);
    }

    loadMapScenario() {
        this.locationMap = new Microsoft.Maps.Map(".warehouse-container__warehouse-map", {
            credentials: "Ai_kbyJazm-AL2ZfyyDNeFHIynRGUGHc6nU5UsYrfKKBQx68-uLk3cLJ5Q1s6roL",
            disableScrollWheelZoom: true
        });
    }
}