import {Injectable} from "@angular/core";
import {Students} from "./students";

@Injectable()

export class StudentService {
    getStudents() {
        return Promise.resolve(Students);
    }
}