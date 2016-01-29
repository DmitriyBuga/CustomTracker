app.service("angularService", function ($http) {

    
    this.getStatuses = function () {
        return $http.get("GetAll");
    };

    
    this.getStatus = function (status) {
        
        var response = $http({
            method: "post",
            url: "GetStatusById",
            params: {
                id: JSON.stringify(status.Id)
            }
        });
        return response;
    }

    
    this.updateStatus = function (status) {
        
        var response = $http({
            method: "post",
            url: "UpdateStatus",
            data: JSON.stringify({ statusName: status.Status, Id: status.Id }),
            dataType: "json"
        });
        return response;
    }

    
    this.AddStatus = function (status) {
        var response = $http({
            method: "post",
            url: "AddStatus",
            data: JSON.stringify({ statusName: status.Status, Id:status.Id }),
            dataType: "json"
        });
        return response;
    }

    
    this.DeleteStatus = function (statusId) {
        alert(statusId);
        var response = $http({
            method: "post",
            url: "DeleteStatus",
            params: {
                id: JSON.stringify(statusId)
            }
        });
        return response;
    }
});