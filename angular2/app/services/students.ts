export interface Student {
    keyId: number,
    name: string,
    class: string,
    email: string
}

export const Students: Student[] = [
    { keyId: 1, name: "John", class: "11050301", email: "John@gmail.com" },
    { keyId: 2, name: "Michael", class: "11050301", email: "Michael@gmail.com" },
    { keyId: 3, name: "Barack", class: "11050302", email: "Barack@gmail.com" },
    { keyId: 4, name: "Geoge", class: "11050302", email: "Geoge@gmail.com" },
    { keyId: 5, name: "Nancy", class: "11050301", email: "Nancy@gmail.com" }
];