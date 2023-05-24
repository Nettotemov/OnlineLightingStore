(function () {
	window.QuillFunctions = {
		createQuill: function (quillElement) {
			var options = {
				debug: 'info',
				modules: {
					toolbar: '#toolbar'
				},
				placeholder: 'Compose an epic...',
				readOnly: false,
				theme: 'snow'
			};
			// set quill at the object we can call
			// methods on later
			new Quill(quillElement, options);
		},
		getQuillContent: function (quillControl) {
			return JSON.stringify(quillControl.__quill.getContents());
		},
		getQuillHTML: function (quillControl) {
			return quillControl.__quill.root.innerHTML;
		},
		loadQuillContent: function (quillControl, quillContent) {
			content = JSON.parse(quillContent);
			// console.log(content);
			// const encodedHTML = decodeEntities(content);
			// console.log(encodedHTML);
			// return quillControl.__quill.updateContents(content);
			return quillControl.__quill.root.innerHTML = content;
		}
	};
})();

// var decodeEntities = (function () {
// 	// this prevents any overhead from creating the object each time
// 	var element = document.createElement('div');

// 	function decodeHTMLEntities(str) {
// 		if (str && typeof str === 'string') {
// 			// strip script/html tags
// 			str = str.replace(/<script[^>]*>([\S\s]*?)<\/script>/gmi, '');
// 			str = str.replace(/<\/?\w(?:[^"'>]|"[^"]*"|'[^']*')*>/gmi, '');
// 			element.innerHTML = str;
// 			str = element.textContent;
// 			element.textContent = '';
// 		}

// 		return str;
// 	}

// 	return decodeHTMLEntities;
// })();