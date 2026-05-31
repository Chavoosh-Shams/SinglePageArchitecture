// Create Modal Elements 
const firstNameInput = document.querySelector("#firstNameInput");
const lastNameInput = document.querySelector("#lastNameInput");
const createBtn = document.querySelector(".create-btn");

// Update Modal Elements 
const firstNameUpdateModalInput = document.querySelector(".firstName-update-modal");
const lastNameUpdateModalInput = document.querySelector(".lastName-update-modal");
const updateBtn = document.querySelector(".update-btn");

// Delete Modal Element
const removeBtn = document.querySelector(".remove-btn");

// Details Modal Elements 
const detailId = document.querySelector("#detailId");
const detailFirstName = document.querySelector("#detailFirstName");
const detailLastName = document.querySelector("#detailLastName");

// PersonCount 
const personCount = document.querySelector("#personCount");

// Table Body Elem For Inserting Html Records
const tableBody = document.querySelector(".table-body");

// Global Variables
let selectedPersonId = null;

// Base URL for AJAX Fetch Requests
const baseUrl = "http://localhost:5140/Person";


// Ajax : Fetch All Persons 
const fetchPersons = async () => {

    try {

        const response = await fetch(`${baseUrl}/GetAll`);

        const data = await response.json();

        showPersons(data);

    } catch (error) {

        console.error(error);

    }

    // Befor ES8 2017

    //fetch(`${baseUrl}/GetAll`)

    //.then(response => response.json())

    //.then(data => showPersons(data))

    //.catch((error) => {

    //    console.error(error);

    //});

}


// Render Last Three Persons 
const showPersons = persons => {

    tableBody.innerHTML = "";

    personCount.innerHTML = `There are ${persons.length} Persons in DataBase`;

    const lastPersons = persons.slice(-3);

    lastPersons.forEach(person => {

        tableBody.insertAdjacentHTML("beforeend",
            `
                <tr>
                    <td>${person.firstName}</td>
                    <td>${person.lastName}</td>
                    <td>
                        <button class="btn btn-danger" data-bs-toggle="modal" data-bs-target="#deleteModal" onclick="setPersonId('${person.id}')">Remove</button>
                        <button class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#updateModal" onclick="populateUpdateModal('${person.id}','${person.firstName}','${person.lastName}')">Update</button>
                        <button class="btn btn-info text-white" data-bs-toggle="modal" data-bs-target="#detailsModal" onclick="showDetail('${person.id}')">Details</button>
                    </td>
                </tr>
            `
        );

    })
}


// Ajax : Create Person
const createPerson = async () => {

    try {

        const validation = validationInput(
            firstNameInput.value,
            lastNameInput.value
        );

        if (!validation.isValid) {
            alert(validation.message);
            return;
        }

        // Generate a unique GUID
        const id = crypto.randomUUID();

        const newPerson = {
            id: id,
            firstName: firstNameInput.value.trim(),
            lastName: lastNameInput.value.trim()
        };

        const response = await fetch(`${baseUrl}/PostPerson`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            },
            body: JSON.stringify(newPerson),
        });

        if (response.ok) {
            const modal = bootstrap.Modal.getOrCreateInstance(document.getElementById("createPersonModal"));
            modal.hide();
            fetchPersons();
        } else {
            throw new Error("Request failed");
        }

        inputCleaner();

    } catch (error) {
        console.error(error);
    }

}


// Ajax : Update Person
const updatePerson = async () => {

    try {

        const validation = validationInput(
            firstNameUpdateModalInput.value,
            lastNameUpdateModalInput.value
        );

        if (!validation.isValid) {
            alert(validation.message);
            return;
        }

        const updatePerson = {
            id: selectedPersonId,
            firstName: firstNameUpdateModalInput.value.trim(),
            lastName: lastNameUpdateModalInput.value.trim()
        };

        const response = await fetch(`${baseUrl}/PutPerson/${selectedPersonId}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            },
            body: JSON.stringify(updatePerson),
        });

        if (response.ok) {
            const modal = bootstrap.Modal.getOrCreateInstance(document.getElementById("updateModal"));
            modal.hide();
            fetchPersons();
        } else {
            throw new Error("Request failed");
        }

    } catch (error) {

        console.error(error);

    }

}


// Ajax : Delete Person
const removePerson = async () => {

    try {

        const response = await fetch(`${baseUrl}/DeletePerson/${selectedPersonId}`, {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        });

        if (response.ok) {
            const modal = bootstrap.Modal.getOrCreateInstance(document.getElementById("deleteModal"));
            modal.hide();
            fetchPersons();
        } else {
            throw new Error("Request failed");
        }

    } catch (error) {
        console.error(error);
    }

}


// Ajax : Get Detail Of Person
const showDetail = async (personId) => {

    try {

        const response = await fetch(`${baseUrl}/GetPersonById/${personId}`);

        const person = await response.json();

        detailId.innerHTML = person.id;

        detailFirstName.innerHTML = person.firstName;

        detailLastName.innerHTML = person.lastName;

    } catch (error) {

        console.error(error);

    }

}


// Functions: Save Person ID for Edit or Remove
const setPersonId = personId => {
    selectedPersonId = personId;
}


// Functions: Populate Update Modal Inputs and Store Selected Person ID
const populateUpdateModal = (personId, personFirstName, personLastName) => {
    setPersonId(personId);
    firstNameUpdateModalInput.value = personFirstName;
    lastNameUpdateModalInput.value = personLastName;
}


// Functions: Clear Form Inputs 
const inputCleaner = () => {
    firstNameInput.value = "";
    lastNameInput.value = "";
}


// Validation
const validationInput = (firstName, lastName) => {

    const nameRegex = /^[a-zA-Zآ-ی\s]+$/;

    firstName = firstName.trim();
    lastName = lastName.trim();

    if (!firstName || !lastName) {
        return {
            isValid: false,
            message: "نام و نام خانوادگی الزامی است"
        };
    }

    if (firstName.length < 2 || lastName.length < 2) {
        return {
            isValid: false,
            message: "نام و نام خانوادگی باید حداقل ۲ کاراکتر باشند"
        };
    }

    if (!nameRegex.test(firstName) || !nameRegex.test(lastName)) {
        return {
            isValid: false,
            message: "فقط حروف مجاز هستند"
        };
    }

    return {
        isValid: true
    }

}


// Event Listeners
window.addEventListener("load", fetchPersons);
createBtn.addEventListener("click", createPerson);
updateBtn.addEventListener("click", updatePerson);
removeBtn.addEventListener("click", removePerson);