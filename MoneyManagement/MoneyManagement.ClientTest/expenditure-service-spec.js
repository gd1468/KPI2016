describe('expenditure service', function () {
    var $scope,
        $rootScope,
        expenditureService,
        $httpBackend;
    beforeEach(module('expenditureServices'));

    beforeEach(inject(function (_$rootScope_, _expenditureService_, _$httpBackend_) {
        $rootScope = _$rootScope_;
        $scope = $rootScope.$new();
        expenditureService = _expenditureService_;
        $httpBackend = _$httpBackend_;
    }));

    it("should create new expenditure record be defined ", function () {
        var promise = (expenditureService.createNewExpenditureRecord({}).then);
        expect(promise).toBeDefined();
    });

    it("should deposit existing account be defined ", function () {
        var promise = (expenditureService.depositExistingAccount({}).then);
        expect(promise).toBeDefined();
    });

    it("should expenditures be defined ", function () {
        var promise = (expenditureService.expenditures().then);
        expect(promise).toBeDefined();
    });

    it("should delete expenditure be defined ", function () {
        var promise = (expenditureService.deleteExpenditure({}).then);
        expect(promise).toBeDefined();
    });

    it("should update expenditure be defined ", function () {
        var promise = (expenditureService.updateExpenditure({}).then);
        expect(promise).toBeDefined();
    });
});