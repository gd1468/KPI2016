describe('expenditure account controller', function () {
    var $scope,
        $rootScope,
        controller,
        $httpBackend;

    // load module which contain directive
    beforeEach(function () {
        module('expenditureApp');
        module('expenditureServices');
    });

    beforeEach(inject(function ($injector) {
        $rootScope = $injector.get('$rootScope');
        $rootScope.resetForm = function () { };
        $scope = $rootScope.$new();
        var baseController = $injector.get('$controller');
        controller = baseController('expenditureAccountController', { $scope: $scope });
        $scope.form = {
            $valid: true,
            $setPristine: function () { },
            $setUntouched: function () { }
        };
        $httpBackend = $injector.get('$httpBackend');
        $httpBackend.when('GET', '/api/CultureApi').respond(200, { keyId: 1 });
        $httpBackend.when('POST', '/api/AccountApi?params=%7B%22cultureId%22:%22@cultureId%22%7D').respond({
            AccountPresentations: [
                {
                    amount: 1000,
                    name: "new",
                    shortName: "N"
                }
            ]
        });
        $scope.user = {};
        $scope.culture = {};

    }));

    it('should change flag to true', function () {
        $scope.isAllSelected = false;
        $scope.accounts = [
        {
            selected: false
        }];
        $scope.toggleAll();
        expect($scope.isAllSelected).toBeTruthy();
        expect($scope.accounts[0].selected).toBeTruthy();
    });

    it('should change flag to false', function () {
        $scope.isAdding = true;
        $scope.addNew();
        expect($scope.isAdding).toBeFalsy();
    });

    it('should change flag to false', function () {
        $scope.user.keyId = 1;
        $scope.culture.KeyId = 1;
        $scope.newAccount = {
            amount: 1000,
            name: "new",
            shortName: "N"
        };

        $scope.addAccount($scope.form);
        $httpBackend.flush();
        expect($scope.isAdding).toBeTruthy();
    });
});