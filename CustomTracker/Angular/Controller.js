app.controller("myCntrl", function ($scope, angularService) {
    $scope.divStatus = false;
    GetAllStatuses();
    //To Get All Records  
    function GetAllStatuses() {
        var getData = angularService.getStatuses();
        
        getData.then(function (emp) {
            $scope.statuses = emp.data;
        },function () {
            alert('Error in getting records');
        });
    }

    $scope.editStatus = function (status) {
        var getData = angularService.getStatus(status);
        getData.then(function (status) {
            $scope.status = status.data;
            $scope.statusId = status.data.Id;
            $scope.statusStatus = status.data.Status;
            $scope.Action = "Update";
            $scope.divStatus = true;
        }, function () {
            alert('Error in getting records');
        });
    }

    $scope.AddUpdateStatus = function ()
    {
        debugger;
        var Status = {
            Id: $scope.statusId,
            Status: $scope.statusStatus,
        };
        
        var getAction = $scope.Action;

        if (getAction == "Update") {
            alert(status.statusId);
            var getData = angularService.updateStatus(Status);
            getData.then(function (msg) {
                GetAllStatuses();
                $scope.divStatus = false;
            }, function () {
                alert('Error in updating record');
            });
        } else {
            Status.Id = 0;
            var getData = angularService.AddStatus(Status);
            getData.then(function (msg) {
                GetAllStatuses();
                $scope.divStatus = false;
            }, function () {
                alert('Error in adding record');
            });
        }
    }
    $scope.CloseStatusDiv = function ()
    {
        $scope.divStatus = false;
    }
    $scope.AddStatusDiv = function ()
    {
        ClearFields();
        $scope.Action = "Add";
        $scope.divStatus = true;
    }

    $scope.deleteStatus = function (status)
    {
        var getData = angularService.DeleteStatus(status.Id);
        getData.then(function (msg) {
            GetAllStatuses();
            alert('Status Deleted');
        },function(){
            alert('Error in Deleting Record');
        });
    }

    function ClearFields() {
        $scope.statusId = "";
        $scope.statusStatus = "";
    }
});