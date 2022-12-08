/* Обработчик клика на чекбоксах */
$('input[data-check]').on('input', function () {
	let aChecked = [];
	$('input[data-check]:checked').each(function () { aChecked.push($(this).data('check')); });
	localStorage.setItem('CheckboxChecked', aChecked.join(';'));
	localStorage.setItem('PasswordLength', $('#height_opt').val());
});

/* Установка состояний чекбоксов, после загрузки страницы */
// $(document).ready(function () {
// 	if (localStorage.getItem('CheckboxChecked')) {
// 		$('input[data-check]').prop('checked', false);
// 		let aChecked = localStorage.getItem('CheckboxChecked').split(';');
// 		aChecked.forEach(function (str) { $('input[data-check="' + str + '"]').prop('checked', true); });
// 	}
// 	if (localStorage.getItem('PasswordLength')) {
// 		$('#height_opt').val(localStorage.getItem('PasswordLength'));
// 	}
// });

/* Установка состояний чекбоксов, при переходе на сайт по ссылке */
function autoCheck() {
	const urlParams = new URL(window.location.href);
	let checked = urlParams.searchParams.getAll("tags");
	let colorChecked = urlParams.searchParams.getAll("color");
	let typesChecked = urlParams.searchParams.getAll("types");
	let materialsChecked = urlParams.searchParams.getAll("materials");
	if (!checked || !colorChecked || !typesChecked || !materialsChecked) {
		return;
	}
	for (let i = 0; i < checked.length; i++) {
		var target = document.querySelector('input[type="checkbox"][id="' + checked[i] + '"]').checked = true;
	}
	for (let i = 0; i < colorChecked.length; i++) {
		var target = document.querySelector('input[type="checkbox"][id="' + colorChecked[i] + '"]').checked = true;
	}
	for (let i = 0; i < typesChecked.length; i++) {
		var target = document.querySelector('input[type="checkbox"][id="' + typesChecked[i] + '"]').checked = true;
	}
	for (let i = 0; i < materialsChecked.length; i++) {
		var target = document.querySelector('input[type="checkbox"][id="' + materialsChecked[i] + '"]').checked = true;
	}
	if (!target) {
		return;
	}
	target.checked = true;
}
autoCheck();

const createURL = () => {
	let urlSearch = new URLSearchParams()
	let name = $("[name=name]:input").map((_, select) => select.value).get()
	let category = $("[name=category]").map((_, select) => select.value).get()
	let types = $("[name=types]:checked").map((_, chk) => chk.value).get()
	let materials = $("[name=materials]:checked").map((_, chk) => chk.value).get()
	let tags = $("[name=tags]:checked").map((_, chk) => chk.value).get()
	let color = $("[name=color]:checked").map((_, chk) => chk.value).get()
	let sorted = $("[name=sortOrder]").map((_, select) => select.value).get()
	let minPrice = $("[name=minPrice]:input").map((_, minPrice) => minPrice.value).get()
	let maxPrice = $("[name=maxPrice]:input").map((_, maxPrice) => maxPrice.value).get()
	if (name.length > 0) urlSearch.set("name", name.join("&name="))
	if (category.length > 0) urlSearch.set("category", category.join("&category="))
	if (types.length > 0) urlSearch.set("types", types.join("&types="))
	if (materials.length > 0) urlSearch.set("materials", materials.join("&materials="))
	if (tags.length > 0) urlSearch.set("tags", tags.join("&tags="))
	if (color.length > 0) urlSearch.set("color", color.join("&color="))
	if (sorted.length > 0) urlSearch.set("sortOrder", sorted.join("&sortOrder="))
	if (minPrice > 0) urlSearch.set("minPrice", minPrice.join("&minPrice="))
	if (maxPrice > 0) urlSearch.set("maxPrice", maxPrice.join("&maxPrice="))
	const srch = '?' + urlSearch.toString();
	console.log(srch);
	window.history.pushState({}, '', srch.replace(/%26/g, "&").replace(/%3D/g, "="));
};
$(function () {
	$("input:checkbox, [name=category], [name=sortOrder], [name=minPrice], [name=maxPrice]").on("change click", createURL)
});



$(document).on("submit", ".addToCart", function (ev) {
	var frm = $(this);
	$.ajax({
		type: 'POST',
		url: '/Cart',
		data: frm.serialize(),
		dataType: "json",
		success: function (str) {
			let jsonCart = str;
			$('#quantity').html(jsonCart.quantity);
			// $('#sumcart').html(new Intl.NumberFormat('ru-RU', { style: 'currency', currency: 'RUB' }).format(jsonCart.sumCart)); общ. стоимость корзины в header
		}
	});
	ev.preventDefault();
});

$(document).on("click", ".pagination-btn", function (ev) {
	$('#spinner-border').show();
	let path = ev.target.baseURI.split("?"); // забираем путь
	let page = ev.target.value;
	var token = GetAntiForgeryToken();
	let prodview = document.getElementById('prod-view');
	if (typeof page !== 'undefined') {
		$.ajax({
			type: 'GET',
			url: '/Catalog/' + page + "?handler=Products",
			data: path[1], // забираем страницу, которую нужно отобразить
			dataType: 'json',
			success: function (pages) {
				let getUrl = '/Catalog/' + page + "?" + path[1];
				window.history.pushState({}, '', getUrl); // устанавливаем URL в строку браузера
				let productJson = JSON.parse(pages.json)
				let postWrp;
				if (productJson.length == 0) {
					$('#prod-view').empty();
					postWrp = document.createElement("div");
					postWrp.classList.add("col-12");
					postWrp.innerHTML = `<div class="" id="">
					<p class="">Нет товаров</p></div>`
					prodview.append(postWrp);
					$('#spinner-border').hide();
				}
				if (productJson.length > 0) {
					$('#prod-view').empty();
					for (let i = 0; i < productJson.length; i++) {
						let price = productJson[i].Price;
						let oldPrice = productJson[i].OldPrice;
						postWrp = document.createElement("div");
						postWrp.classList.add("col-3");
						postWrp.innerHTML = `<div class="" id="">
						<div id="img-container">
    						<div class="title-wrp">${productJson[i].Name}</div>
    							<div class="post-and-image">
    								<img class="col-12" src="${productJson[i].MainPhoto}">
    								<div class="description-wrp">${productJson[i].Description}
 								</div>
  							</div>
  							<div class="footer-row">
        						<div class="tags-wrp">${productJson[i].Tags}</div>
								<div class="tags-wrp">${price.toLocaleString('ru-RU', { style: 'currency', currency: 'RUB' })}</div>
								<div class="tags-wrp">${productJson[i]?.Discount ?? ""}</div>
								<div class="tags-wrp"><s>${oldPrice?.toLocaleString('ru-RU', { style: 'currency', currency: 'RUB' }) ?? ""}</s></div>
    						</div>
						</div>
						<a href="/Catalog/${productJson[i].Category.CategoryName}/${productJson[i].Name}/${productJson[i].ProductID}">Подробнее</a>
						<form id="ProductID=${productJson[i].ProductID}" class="addToCart" method="post" action="/Cart" >
							<input type="hidden" name="ProductID" id="pr_ProductID" value="${productJson[i].ProductID}" />
							<input type="hidden" name="returnUrl" value="${document.location.href}" />
							<button type="submit" class="btn btn-success btn-sm pull-right" style="float:right">
								Добавить в корзину
							</button>
							<input name="${token.name}" type="hidden" value="${token.value}">
						</form>`
						prodview.append(postWrp);
						$('#spinner-border').hide();
					}

				}
			}
		});
	}
	ev.preventDefault();
});

$('.catalogForm-sub').on('change click', '.autocomplete__results-item, .autocomplete__clear, input[type=checkbox], [name=category], [name=sortOrder], [name=minPrice], [name=maxPrice]', '', function (ev) {
	$('#spinner-border').show();
	let searchName = document.querySelector(".searchByName");
	let name = $(this).attr('data-value') ?? "";
	searchName.value = name;
	let autocompleteClear = document.querySelector('.autocomplete__clear');
	if (searchName.value != '') {
		autocompleteClear.hidden = false;
		$('input:checked').prop('checked', false);
		$('input[type=number]').each( function () { $(this).val(''); });
		$('#select-category option:first').prop('selected', true);
	}
	else {
		autocompleteClear.hidden = true;
	}
	createURL();
	let path = ev.target.baseURI.split("?"); // забираем путь
	let getUrl = '/Catalog/' + 1 + "?" + path[1];
	let url = getUrl + "&handler=Products";
	var token = GetAntiForgeryToken();
	let prodview = document.getElementById('prod-view');
	console.log(prodview);
	let response = fetch(url)
		.then((response) => response.json())
		.then(data => {
			let pagingJson = JSON.parse(data.pagingInfoJson)
			window.history.pushState({}, '', getUrl); // устанавливаем URL в строку браузера
			let paging = document.getElementById('pagingbuttons');
			$('#pagingbuttons').empty();
			for (let i = 1; i <= pagingJson.TotalPages; i++) {
				pagingButtons = document.createElement("li");
				pagingButtons.classList.add("page-item");
				pagingButtons.innerHTML = `<button form="catalogForm" class="btn pagination-btn btn-default" type="submit" value="${i}" formaction="/${i}">${i}</button>`
				paging.appendChild(pagingButtons);
			}
			let placeholderMinPrice = document.getElementById('minPrice')
			placeholderMinPrice.placeholder = pagingJson.PlaceholderMinPrice;
			let placeholderMaxPrice = document.getElementById('maxPrice')
			placeholderMaxPrice.placeholder = pagingJson.PlaceholderMaxPrice;

			let productJson = JSON.parse(data.json);
			let postWrp;
			if (productJson.length == 0) {
				$('#prod-view').empty();
				postWrp = document.createElement("div");
				postWrp.classList.add("col-12");
				postWrp.innerHTML = `<div class="" id="">
				<p class="">Нет товаров</p></div>`
				prodview.append(postWrp);
				$('#spinner-border').hide();
			}
			if (productJson.length > 0) {
				$('#prod-view').empty();
				for (let i = 0; i < productJson.length; i++) {
					let price = productJson[i].Price;
					let oldPrice = productJson[i].OldPrice;
					postWrp = document.createElement("div");
					postWrp.classList.add("col-3");
					postWrp.innerHTML = `<div class="" id="">
					<div id="img-container">
    					<div class="title-wrp">${productJson[i].Name}</div>
    						<div class="post-and-image">
    							<img class="col-12" src="${productJson[i].MainPhoto}">
    							<div class="description-wrp">${productJson[i].Description}
 							</div>
  						</div>
  						<div class="footer-row">
        					<div class="tags-wrp">${productJson[i].Tags}</div>
							<div class="tags-wrp">${price.toLocaleString('ru-RU', { style: 'currency', currency: 'RUB' })}</div>
							<div class="tags-wrp">${productJson[i]?.Discount ?? ""}</div>
							<div class="tags-wrp"><s>${oldPrice?.toLocaleString('ru-RU', { style: 'currency', currency: 'RUB' }) ?? ""}</s></div>
    					</div>
					</div>
					<a href="/Catalog/${productJson[i].Category.CategoryName}/${productJson[i].Name}/${productJson[i].ProductID}">Подробнее</a>
					<form id="ProductID=${productJson[i].ProductID}" class="addToCart" method="post" action="/Cart" >
						<input type="hidden" name="ProductID" id="pr_ProductID" value="${productJson[i].ProductID}" />
						<input type="hidden" name="returnUrl" value="${document.location.href}" />
						<button type="submit" class="btn btn-success btn-sm pull-right" style="float:right">
							Добавить в корзину
						</button>
						<input name="${token.name}" type="hidden" value="${token.value}">
					</form>`
					prodview.append(postWrp);
					$('#spinner-border').hide();
				}

			}
		})
});


$('.searchByName').on('input', function (ev) {
	let searchName = document.querySelector(".searchByName").value
	let getUrl = '/Catalog/?'// забираем путь
	let url = getUrl + "handler=AllProducts";
	let autocompleteResults = document.getElementById('autocomplete__results');
	let autocomplete;
	let autocompleteClear = document.querySelector('.autocomplete__clear');
	let response = fetch(url)
		.then((response) => response.json())
		.then(data => {
			let productJson = JSON.parse(data.json);
			if (productJson.length > 0) {
				$('#autocomplete__results').empty();
				let namesArr = [];
				let elementIs = true;
				for (let i = 0; i < productJson.length; i++) {
					let productName = productJson[i].Name.toUpperCase();
					if (productName.includes(searchName.toUpperCase())) {
						namesArr.push(productName);
					}
				}
				if (searchName.length > 0) {
					autocompleteClear.hidden = false;
				}
				else {
					autocompleteClear.hidden = true;
				}
				if (elementIs === true && searchName.length > 0) {
					if (namesArr.length > 0) {
						for (let i = 0; i < namesArr.length; i++) {
							autocomplete = document.createElement("li");
							autocomplete.classList.add("autocomplete__results-item");
							autocomplete.setAttribute("data-value", namesArr[i])
							autocomplete.innerHTML = `${namesArr[i]}`
							autocompleteResults.append(autocomplete);
							autocompleteResults.hidden = false;
						};
					}
					else {
						autocomplete = document.createElement("li");
						autocomplete.classList.add("autocomplete__results-item");
						autocomplete.innerHTML = `Нет результатов.`
						autocompleteResults.append(autocomplete);
						autocompleteResults.hidden = false;
					}
				}
				$(document).on('click', ".autocomplete__results-item, .autocomplete__clear", function (e) {
					namesArr.splice(0, namesArr.length);
					elementIs = false;
					autocompleteResults.hidden = true;
				})
			}
		})
});

function GetAntiForgeryToken() {
	var tokenField = $("input[type='hidden'][name$='RequestVerificationToken']");
	if (tokenField.length == 0) {
		return null;
	} else {
		return {
			name: tokenField[0].name,
			value: tokenField[0].value
		};
	}
}

$(function () {
	var header = $(".header");
	// let navWrapper = document.getElementById('navWrapper');
	let scroll = $(window).scrollTop();
	// var headerDark = document.getElementById('footer-bg').scrollHeight;

	if (scroll > 10) {
		header.addClass("scrolled");
	} else {
		header.removeClass("scrolled");
	}

	$(window).scroll(function () {
		scroll = $(window).scrollTop();

		if (scroll > 10) {
			header.addClass("scrolled");
		}
		// else if (scroll >= headerDark)
		// {
		// 	navWrapper.remove();
		// }
		else {
			header.removeClass("scrolled");
			// $('.header__wrapper').append(navWrapper);
		}
	});

});


// /* Обработчик клика на чекбоксах */
// $('input').on('input', function () {
// 	let aChecked = [];
// 	$('input[type="checkbox"]:checked').each(function () { aChecked.push($(this).getPath()); });
// 	localStorage.setItem('CheckboxChecked', aChecked.join(';'));
// 	localStorage.setItem('PasswordLength', $('#height_opt').val());
// });

// /* Установка состояний чекбоксов, после загрузки страницы */
// $(document).ready(function () {
// 	if (localStorage.getItem('CheckboxChecked')) {
// 		let aChecked = localStorage.getItem('CheckboxChecked').split(';');
// 		$('input[type="checkbox"]').prop('checked', false);
// 		aChecked.forEach(function (str) { $(str).prop('checked', true); });
// 	}
// 	if (localStorage.getItem('PasswordLength')) {
// 		$('#height_opt').val(localStorage.getItem('PasswordLength'));
// 	}
// });

/************************************************************
 * Функция для jQ, возвращающая уникальный селектор элемента *
 * Источник: https://stackoverflow.com/a/26762730/10179415   *
 ************************************************************/
// jQuery.fn.extend({
// 	getPath: function () {
// 		let pathes = [];
// 		this.each(function (index, element) {
// 			let path, $node = jQuery(element);
// 			while ($node.length) {
// 				let realNode = $node.get(0), name = realNode.localName;
// 				if (!name) { break; }
// 				name = name.toLowerCase();
// 				let parent = $node.parent();
// 				let sameTagSiblings = parent.children(name);
// 				if (sameTagSiblings.length > 1) {
// 					let allSiblings = parent.children();
// 					let index = allSiblings.index(realNode) + 1;
// 					if (index > 0) { name += ':nth-child(' + index + ')'; }
// 				}
// 				path = name + (path ? '>' + path : '');
// 				$node = parent;
// 			}
// 			pathes.push(path);
// 		});
// 		return pathes.join(',');
// 	}
// });
