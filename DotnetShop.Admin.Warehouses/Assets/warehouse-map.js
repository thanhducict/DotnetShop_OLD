export class WarehouseMap {
    constructor() {
        this.locationMap = {};
        this.locationInfobox = {};
        this.locationInfo = "";
        this.markers = [];
    }

    init() {
        if (document.querySelector(".warehouse-container__warehouse-map") === null) {
            return;
        }

        let instance = this;
        instance.loadScript("https://www.bing.com/api/maps/mapcontrol?&callback=getMap");
        window.getMap = () => {
            instance.loadMapScenario();
        };

        document.querySelector(".warehouse-container__dropdown select").addEventListener("change", (e) => {
            this.showLocations(e.target.value);
        });
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

        this.locationInfobox = new Microsoft.Maps.Infobox(this.locationMap.getCenter(), { visible: false });
        this.locationInfobox.setMap(this.locationMap);
        this.showLocations(document.querySelectorAll(".warehouse-container__dropdown select option")[0].value);
    }

    showLocations(productId) {
        let locations = [];
        let instance = this;
       
        fetch(`https://localhost:7025/Admin/Warehouses/GetWarehouseMapInfo?productId=${productId}`)
            .then((response) => response.json())
            .then((data) => {
                this.markers = [];
                let warehouses = data.warehouseModels;
                warehouses.forEach((item) => {
                    locations.push({
                        name: item.warehouseName,
                        address: item.address,
                        lat: item.latitude,
                        lon: item.longitude,
                    });
                });

                for (var i = this.locationMap.entities.getLength() - 1; i >= 0; i--) {
                    var pushpin = this.locationMap.entities.get(i);
                    if (pushpin instanceof Microsoft.Maps.Pushpin) {
                        this.locationMap.entities.removeAt(i);
                    }
                }

                for (let i = 0; i < locations.length; i++) {
                    const loc = locations[i];
                    var locationCoordinates = new Microsoft.Maps.Location(loc.lat, loc.lon);
                    let pushpin = new Microsoft.Maps.Pushpin(locationCoordinates, {});
                    Microsoft.Maps.Events.addHandler(pushpin, "click", (e) => {
                        instance.locationInfobox.setOptions({
                            location: e.target.getLocation(),
                            title: "Address",
                            maxHeight: 300,
                            maxWidth: 280,
                            description: loc.address,
                            visible: true
                        });
                    });
                    this.locationMap.entities.push(pushpin);
                    this.markers.push(locationCoordinates);
                    this.locationMap.setView({
                        bounds: new Microsoft.Maps.LocationRect.fromLocations(this.markers)
                    });
                }
            });
    }
}