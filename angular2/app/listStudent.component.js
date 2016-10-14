"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var student_service_component_1 = require('./services/student-service.component');
var StudentListComponent = (function () {
    function StudentListComponent(_studentService) {
        var _this = this;
        this._studentService = _studentService;
        this._studentService.getStudents().then(function (students) { return _this.students = students; });
    }
    StudentListComponent = __decorate([
        core_1.Component({
            selector: 'student-list',
            templateUrl: '../views/StudentList.html',
            providers: [student_service_component_1.StudentService]
        }), 
        __metadata('design:paramtypes', [student_service_component_1.StudentService])
    ], StudentListComponent);
    return StudentListComponent;
}());
exports.StudentListComponent = StudentListComponent;
//# sourceMappingURL=listStudent.component.js.map