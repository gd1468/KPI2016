import { Component } from '@angular/core';
import { StudentService } from './services/student-service.component';
import {Student} from './services/students';

@Component({
    selector: 'student-list',
    templateUrl: '../views/StudentList.html',
    providers: [StudentService]
})

export class StudentListComponent {
    public students: Student[];

    constructor(private _studentService: StudentService) {
        this._studentService.getStudents().then((students: Student[]) => this.students = students);
    }
}