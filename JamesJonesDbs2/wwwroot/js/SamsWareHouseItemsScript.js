//Instanciate stuff once window loads
window.addEventListener('load', (e) => {
    console.log("Is this even going Through?")
    //Adds Event Listeners to the buttons
    AddButtonEventListeners();
    
    //Adds event listener to submit the form of adding an item to list (WORKS)
    document.getElementById('addShopItemToListForm').addEventListener('submit', async (e) => {
        handleAddShopItemToList(e);
    })

    //Spawns the create modal (WORKS - but Breaks when not Admin)
    document.getElementById('btnShowCreateModal').addEventListener('click', async (e) => {
        console.log("Event Fired - Spawn Create Item Modal");
        await ShowCreateItemModal(e);
    });

    
    
})

//Instanciates the DDL modal 
async function addToShoppingList(itemId) {
    sessionStorage.setItem('selectedItemId', itemId);
    //show Modal
    $('#addShopItemToListModal').modal('show');
    //get DDL controller method to show
    let result = await advFetch('/ShoppingList/ShoppingListDDL');
    let resultHtml = await result.text();

    document.getElementById('ddlContainer').innerHTML = resultHtml;

}

//handle add to shoppinglist
async function handleAddShopItemToList(e) {
    e.preventDefault();

    //Spinner Show
    let button = document.getElementById('btnAddItemToShoppingList');
    button.setAttribute('disabled', 'disabled')
    button.innerHTML = `
            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
            Processing...
            `;

    //Console Check
    console.log('event fired');

    //Assign Id
    let shopItemId = sessionStorage.getItem('selectedItemId');
    let listID = e.target['shoppingList'].selectedOptions[0].value
    console.log("WareHouseID: " + shopItemId + " List ID: " + listID)

    //null check
    if (listID == 0) {
        button.removeAttribute('disabled');
        button.innerHTML = 'Add';
        return;
    }


    //Assign Values
    let shoppingListItem = {
        ShoppingListId: listID,
        SamsWareHouseItemId: shopItemId,
        Quantity: e.target["ItemQuantity"].value
    }

    console.log("reached Here");

    //Get Controller method and send the JSON configured object
    let result = await advFetch('/ShoppingList/AddItemToShoppingList', {
        method: 'POST',
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify(shoppingListItem)
    });

    //if error
    if (result.status == 400) {
        console.log(result)
        alert('The selected list already contains this item')
        button.removeAttribute('disabled');
        button.innerHTML = 'Add';
        return;

    }
    //if successs
    else if (result.ok) {
        $('#addShopItemToListModal').modal('hide');
        button.removeAttribute('disabled');
        button.innerHTML = 'Add';

    }

}

//Show Edit Modal Form
async function ShowEditModal(id) {
    //Check the selected Id
    console.log("current selected id is: " + id);
    //Send the parameter to the controller
    let response = await advFetch('/SamsWareHouseItem/EditShopItem?id=' + id);
    let htmlResponse = await response.text();
    //Assign the structure of the view to the modal template
    document.getElementById('ModalBody').innerHTML = htmlResponse;
    document.getElementById('ModalTitle').innerHTML = "Edit Item";
    //get form reference
    let formReference = document.querySelector('form[action="/SamsWareHouseItem/EditShopItem"]');
    //check forms on console debug
    console.log(formReference)
    //Once is submitted or done button is clicked, handle by calliing another method
    formReference.addEventListener('submit', (e) => { HandleEditSubmit(e, id) });
    //Show modal
    $('#WareHouseItemModal').modal('show');
}


// Handles the editing of a product
async function HandleEditSubmit(e, id) {
    e.preventDefault();

    
    //Taget is itself
    let form = e.target;

    //null check
    let nullOrWhiteSpaceCheckedOne = e.target["ItemName"].value;
    let ItemName = nullOrWhiteSpaceCheckedOne.trim();
    //null indicate
    if (ItemName.length <= 4 && ItemName.length < 32 || ItemName == "") {
        return alert("Please Dont Leave the Item Name empty.")
    }
    //Unit Null Check
    let nullOrWhiteSpaceCheckedTwo = e.target["Unit"].value;
    let Unit = nullOrWhiteSpaceCheckedTwo.trim();
    if (Unit.length <= 1 && Unit.length < 32 || Unit == "") {
        return alert("Please Dont Leave the Item Unit empty.")
    }

    //Spinner
    let button = document.getElementById('btnSaveEdit');
    button.setAttribute('disabled', 'disabled')
    button.innerHTML = `
                <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                Editing...
                `;

    //Set Data Values
    let itemData = {
        SamsWareHouseItemId: id,
        ItemName: form["ItemName"].value,
        Unit: form["Unit"].value,
        UnitPrice: form["UnitPrice"].value
    };

    //Id check on console
    console.log("ItemId" + id);

    //Send the input based on id given and object from forms
    let response = await advFetch('/SamsWareHouseItem/EditShopItem?id=' + id, {
        method: 'PUT',
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify(itemData)
    });

    //Hide Spinner & reset button
    button.removeAttribute('disabled');
    button.innerHTML = 'Save';

    //hide modal
    $('#WareHouseItemModal').modal('hide');

    //reload page
    location.reload();
}

//Create a WareHouseItem modal - or call the modal basically
async function ShowCreateItemModal() {
    //Call controller partial view
    let response = await advFetch('/SamsWareHouseItem/CreateNewShopItem');

    let htmlResponse = await response.text();
    //Set it to modal template
    document.getElementById('ModalBody').innerHTML = htmlResponse;
    document.getElementById('ModalTitle').innerHTML = 'Create New Product';

    //form reference
    let formReference = document.querySelector('form[action="/SamsWareHouseItem/CreateNewShopItem"]');

    //check form on console
    console.log(formReference);

    //Button event listener when user submits, call another method
    formReference.addEventListener('submit', (e) => { handleCreateSubmit(e) });

    //show the modal
    $('#WareHouseItemModal').modal('show');

}//end spawn modal

//Handle Create WareHouseItem - send the user input to the cntroller to process
async function handleCreateSubmit(e) {
    e.preventDefault();


    //find form
    let form = $(e.target);
    if (!form.valid()) {
        return;
    }

    
    //null check
    let nullOrWhiteSpaceCheckedOne = e.target["ItemName"].value;
    let ItemName = nullOrWhiteSpaceCheckedOne.trim();
    //null indicate
    if (ItemName.length <= 4 && ItemName.length < 32 || ItemName == "") {
        return alert("Please Dont Leave the Item Name empty.")
    }
    //Unit Null Check
    let nullOrWhiteSpaceCheckedTwo = e.target["Unit"].value;
    let Unit = nullOrWhiteSpaceCheckedTwo.trim();
    if (Unit.length <= 1 && Unit.length < 32 || Unit == "") {
        return alert("Please Dont Leave the Item Unit empty.")
    }

    //Spinner show
    let button = document.getElementById('btnCreateItem');
    button.setAttribute('disabled', 'disabled')
    button.innerHTML = `
                        <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                        Adding...
                        `;

    //Assign data based from form
    let WareHouseItemData = {
        ItemName: e.target["ItemName"].value,
        Unit: e.target["Unit"].value,
        UnitPrice: e.target["UnitPrice"].value
    };

    //Post JSON string
    let response = await advFetch('/SamsWareHouseItem/CreateNewShopItem', {
        method: "POST",
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify(WareHouseItemData)
    });

    //if 200,
    if (response.ok) {
        console.log(WareHouseItemData);

        //Hide Spinner
        button.removeAttribute('disabled');
        button.innerHTML = 'Add';

        $('#WareHouseItemModal').modal('hide');

        //reload page
        location.reload();

    }

}//end handle create input

/**
 * Delete function for a product
 */
async function DeleteConfirm(id) {

    
    // Confirm Dialog
    if (confirm("Are you sure you want to delete WareHouse Item with Id: " + id)) {

        //null check
        if (id == null) {
            return;
        }

        //Set Spinner
        let button = document.getElementById('btnRemoveShopItem'+id);
        button.setAttribute('disabled', 'disabled')
        button.innerHTML = `
            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
            Removing...
            `;


        // Fetch Request
        await advFetch('/SamsWareHouseItem/RemoveShopItem?id=' + id, { method: "DELETE" })

        //Hide Spinner
        button.removeAttribute('disabled');
        button.innerHTML = 'Remove';

        //reload page
        location.reload();
    }
}

    async function AddButtonEventListeners() {

        // Specify table entity
        let objectContainer = document.getElementById('ItemTableContainer');

        //Add Buttons
        let AddToShoppingListButtons = objectContainer.querySelectorAll('button[value="AddToShoppingList"]');

        AddToShoppingListButtons.forEach((value) => {
            value.addEventListener('click', (e) => {
                console.log(e.target.dataset.itemid);
                addToShoppingList(e.target.dataset.itemid);
            })
        });

        //Edit Buttons
        let EditButtons = objectContainer.querySelectorAll('button[value="EditItem"]');

        EditButtons.forEach((value, index) => {
            value.addEventListener('click', (e) => {
                ShowEditModal(e.target.dataset.itemid);
                console.log(e.target.value.dataset.itemid);
            })
        });

        //Delete Buttons

        let DeleteButtons = objectContainer.querySelectorAll('button[value="Remove"]');

        DeleteButtons.forEach((value, index) => {
            console.log(value);
            console.log(index);
            console.log("the value of this object is: " + value + " with index: " + index);
            value.addEventListener('click', (e) => {
                DeleteConfirm(e.target.dataset.itemid);
                console.log(e.target.dataset.itemid);
            })
        });


    }
