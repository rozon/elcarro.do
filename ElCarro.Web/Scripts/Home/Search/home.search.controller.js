elcarroApp
   .controller('HomeSearchCtrl', ['$scope', '$http',
       function ($scope, $http) {
           $scope.showResultMsgError = false;
           $scope.singleResults = [];
           $scope.listSearchParts = [{
               "name": "",
               "model": 0,
               "make": 0,
               "year": 0,
               "years": ["Año"],
               "makes": [{ "id": 0, "name": "" }],
               "models": [{ "id": 0, "name": "" }]
           }];
           $scope.actualPage = 1;
           $scope.resultPerPages = 6;
           $scope.numPages = [1];
           $scope.totalPages = 0;

           // Common functions
           /* *************************************************************************************** */
           $scope.closeMsgError = function () {
               $scope.showResultMsgError = false;
           };
           $scope.showMsgError = function () {
               $scope.showResultMsgError = true;
           };
           $scope.hasResult = function () {
               return ($scope.singleResults.length > 0);
           }
           function showResult() {
               var _item = $("#loader-main-row");
               _item.addClass("hidden");
               _item = $("#main-list-row");
               _item.removeClass("hidden");
               _item = $("#main-pagination-row");
               _item.removeClass("hidden");
               _item = $("#error-msg-row");
               _item.removeClass("hidden");
           }

           var safeApply = function (fn, _$scope) {
               var phase = _$scope.$root.$$phase;
               if (phase == '$apply' || phase == '$digest') {
                   if (fn && (typeof (fn) === 'function')) {
                       fn();
                   }
               } else {
                   _$scope.$apply(fn);
               }
           };
           /* *************************************************************************************** */

           // Add new search criteria
           /* *************************************************************************************** */
           $scope.addSearch = function () {
               var _item = {
                   "name": "",
                   "model": 0,
                   "make": 0,
                   "year": 0,
                   "years": ["Año"],
                   "makes": [{ "id": 0, "name": "" }],
                   "models": [{ "id": 0, "name": "" }]
               };
               _getVehicleParts();
               _getMakes(function (_makes) {
                   safeApply(function () {
                       _item.makes = _makes;
                       $scope.listSearchParts.push(_item);
                   }, $scope);
               });
           }

           function _getVehicleParts() {
               $http.post('/Home/GetAllVehicleParts', {}).then(
                   function (response) {
                       var result = {};
                       $(response.data).each(function (i, d) {
                           result[d.Name] = d.Photo;
                       });
                       angular.forEach($scope.listSearchParts, function (value, key) {
                           $('#part-' + key + '-name').autocomplete({
                               data: result
                           });
                       });
                   },
                   function (response) {
                       alert("Error fetching the names of parts");
                   });
           }

           function _getMakes(_callback) {
               $http.post('/Home/GetAllMakes', {}).then(
                   function (response) {
                       var _result = [];
                       $.each(response.data, function (e, d) {
                           _result.push({
                               "id": d.Id,
                               "name": d.Name
                           });
                       });
                       _callback(_result);
                   },
                   function (response) {
                       alert("Error fetching the names of parts");
                   });
           }
           /* *************************************************************************************** */

           // Request for parts
           /* *************************************************************************************** */
           function _searchSingleParts(_page) {
               var _item = $scope.listSearchParts[0];
               var _search = {
                   "NameOrDescription": _item.name,
                   "Model": _item.model,
                   "Make": _item.make,
                   "Year": _item.year,
               }
               $http.post('/Home/SearchSingle',
                   {
                       "actualPage": _page,
                       "resultPerPages": $scope.resultPerPages,
                       "_part": _search
                   }).then(
                   function (response) {
                       $scope.totalPages = response.data.TotalParts / $scope.resultPerPages;
                       if ((response.data.TotalParts % $scope.resultPerPages) > 0) {
                           $scope.totalPages++;
                       }
                       $scope.numPages = [];
                       for (var c = 1; c <= $scope.totalPages; c++) {
                           $scope.numPages.push(c);
                       }
                       $scope.singleResults = response.data.Parts;
                       showResult();
                   },
                   function (response) {
                       $scope.showMsgError();
                   });
           }
           /* *************************************************************************************** */

           // Initial executions
           /* *************************************************************************************** */
           (function Init() {
               _searchSingleParts(1);
               _getVehicleParts();
               _getMakes(function (_makes) {
                   safeApply(function () {
                       $scope.listSearchParts[0].makes = _makes;
                   }, $scope);
               });
           }());
           /* *************************************************************************************** */
       }
   ]);