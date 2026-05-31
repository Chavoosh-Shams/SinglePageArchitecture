// Create Modal Elements 
const titleInput = document.querySelector("#titleInput");
const descriptionInput = document.querySelector("#descriptionInput");
const unitPriceInput = document.querySelector("#unitPriceInput");
const createBtn = document.querySelector(".create-btn");

// Update Modal
const titleUpdateModalInput = document.querySelector(".title-update-modal");
const descriptionRecordUpdateModalInput = document.querySelector(".descriptionRecord-update-modal");
const unitPriceUpdateModalInput = document.querySelector(".unitPrice-update-modal");
const updateBtn = document.querySelector(".update-btn");

// Delete Modal Element
const removeBtn = document.querySelector(".remove-btn");

// Details Modal
const detailId = document.querySelector("#detailId");
const detailTitle = document.querySelector("#detailTitle");
const detailDescriptionRecord = document.querySelector("#detailDescriptionRecord");
const detailUnitPrice = document.querySelector("#detailUnitPrice");

// Table Body Selector
const tableBody = document.querySelector(".table-body");

// Product Count Number
const productCount = document.querySelector("#productCount");

// Global Variables
let selectedProductId = null;

// Base URL for AJAX Fetch Requests
const baseUrl = "http://localhost:5140/Product";


// Ajax: GetAll
const fetchProduct = async () => {

    try {

        const response = await fetch(`${baseUrl}/GetAll`);

        const data = await response.json();

        showProduct(data);

    } catch (error) {
        console.error(error);
    }

}


// Render Last Three Product
const showProduct = products => {

    tableBody.innerHTML = "";

    productCount.innerHTML = `There are ${products.length} Products in Database`;

    let lastProducts = products.slice(-3);

    lastProducts.forEach(product => {

        tableBody.insertAdjacentHTML("beforeend",
            `
                <tr>
                    <td>${product.title}</td>
                    <td>${product.descriptionRecord}</td>
                    <td>${product.unitPrice.toLocaleString()}</td>
                    <td>
                        <button class="btn btn-danger" data-bs-toggle="modal" data-bs-target="#deleteModal" onclick="setSelectedId('${product.id}')">Remove</button>
                        <button class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#updateModal" onclick="populateUpdateModal('${product.id}','${product.title}','${product.descriptionRecord}','${product.unitPrice}')">Update</button>
                        <button class="btn btn-info text-white" data-bs-toggle="modal" data-bs-target="#detailsModal" onclick="showDetail('${product.id}')">Details</button>
                    </td>
                </tr>
            `
        )

    });
}


// Ajax: Create Product
const createProduct = async () => {

    try {

        const validation = validationInput(
            titleInput.value,
            descriptionInput.value,
            unitPriceInput.value,
        );

        if (!validation.isValid) {
            alert(validation.message);
            return;
        }

        // Generate a unique GUID
        const id = crypto.randomUUID();

        const newProduct = {
            id: id,
            title: titleInput.value,
            descriptionRecord: descriptionInput.value,
            unitPrice: unitPriceInput.value
        };

        const response = await fetch(`${baseUrl}/PostProduct`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            },
            body: JSON.stringify(newProduct)
        });

        if (response.ok) {
            const modal = bootstrap.Modal.getOrCreateInstance(document.getElementById("createProductModal"));
            modal.hide();
            fetchProduct();
        } else {
            throw new Error("Request failed");
        }

        clearInputs();

    } catch (error) {
        console.error(error);
    }
}


// Ajax : Update Product
const updateProduct = async () => {

    try {
        const validation = validationInput(
            titleUpdateModalInput.value,
            descriptionRecordUpdateModalInput.value,
            unitPriceUpdateModalInput.value,
        );

        if (!validation.isValid) {
            alert(validation.message);
            return;
        }

        const updatedProduct = {
            id: selectedProductId,
            title: titleUpdateModalInput.value,
            descriptionRecord: descriptionRecordUpdateModalInput.value,
            unitPrice: unitPriceUpdateModalInput.value
        };

        const response = await fetch(`${baseUrl}/PutProduct/${selectedProductId}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            },
            body: JSON.stringify(updatedProduct),
        });

        if (response.ok) {
            const modal = bootstrap.Modal.getOrCreateInstance(document.getElementById("updateModal"));
            modal.hide();
            fetchProduct();
        } else {
            throw new Error("Request failed");
        }
    } catch (error) {
        console.error(error);
    }


}


// Ajax : Remove Product
const removeProduct = async () => {

    try {

        const response = await fetch(`${baseUrl}/DeleteProduct/${selectedProductId}`, {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        });

        if (response.ok) {
            const modal = bootstrap.Modal.getOrCreateInstance(document.getElementById("deleteModal"));
            modal.hide();
            fetchProduct();
        } else {
            throw new Error("Request failed");
        }

    } catch (error) {
        console.error(error);
    }

}


// Ajax : Product Detail
const showDetail = async (productId) => {

    try {

        const response = await fetch(`${baseUrl}/GetProductById/${productId}`);

        const product = await response.json();

        detailId.innerHTML = product.id;
        detailTitle.innerHTML = product.title;
        detailDescriptionRecord.innerHTML = product.descriptionRecord;
        detailUnitPrice.innerHTML = product.unitPrice.toLocaleString();

    } catch (error) {
        console.error(error);
    }

}


// Functions: Populate Update Modal Inputs and Store Selected Product ID
const populateUpdateModal = (productId, productTitle, productDescriptionRecord, productUnitPrice) => {
    setSelectedId(productId);
    titleUpdateModalInput.value = productTitle,
        descriptionRecordUpdateModalInput.value = productDescriptionRecord,
        unitPriceUpdateModalInput.value = productUnitPrice
}


// Function : setSelectedId
const setSelectedId = productId => {
    selectedProductId = productId;
}

// Clear Inputs
const clearInputs = () => {
    titleInput.value = "";
    descriptionInput.value = "";
    unitPriceInput.value = "";
}

// Validation
const validationInput = (title, description, unitPrice) => {

    const nameRegex = /^[a-zA-Zآ-ی\s]+$/;

    const trimmedTitle = title.trim();
    const trimmedDescription = description.trim();
    const trimmedPrice = unitPrice.trim();


    if (!trimmedTitle || !trimmedDescription || !trimmedPrice) {
        return {
            isValid: false,
            message: "نام کالا، توضیحات و قیمت را پر کنید"
        };
    }


    if (trimmedTitle.length < 2 || trimmedDescription.length < 2) {
        return {
            isValid: false,
            message: "نام کالا و توضیحات باید حداقل ۲ کاراکتر باشند"
        };
    }


    if (!nameRegex.test(trimmedTitle)) {
        return {
            isValid: false,
            message: "نام کالا فقط باید شامل حروف باشد"
        };
    }


    if (isNaN(trimmedPrice) || Number(trimmedPrice) <= 0) {
        return {
            isValid: false,
            message: "قیمت باید یک عدد معتبر و بزرگ‌تر از صفر باشد"
        };
    }

    return {
        isValid: true
    };
};



// Event Listeners
window.addEventListener("load", fetchProduct);
createBtn.addEventListener("click", createProduct);
updateBtn.addEventListener("click", updateProduct);
removeBtn.addEventListener("click", removeProduct);