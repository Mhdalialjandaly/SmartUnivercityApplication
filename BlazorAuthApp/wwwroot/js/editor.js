window.editorInstances = {};

window.initializeQuillEditor = function (elementId) {
    // Check if Quill is loaded
    if (typeof Quill === 'undefined') {
        console.error('Quill.js is not loaded');
        return;
    }

    const toolbarOptions = [
        ['bold', 'italic', 'underline', 'strike'],
        ['blockquote', 'code-block'],
        [{ 'header': 1 }, { 'header': 2 }],
        [{ 'list': 'ordered' }, { 'list': 'bullet' }],
        [{ 'script': 'sub' }, { 'script': 'super' }],
        [{ 'indent': '-1' }, { 'indent': '+1' }],
        [{ 'direction': 'rtl' }],
        [{ 'size': ['small', false, 'large', 'huge'] }],
        [{ 'header': [1, 2, 3, 4, 5, 6, false] }],
        [{ 'color': [] }, { 'background': [] }],
        [{ 'font': [] }],
        [{ 'align': [] }],
        ['clean'],
        ['link', 'image', 'video']
    ];

    // Destroy existing editor if it exists
    if (window.editorInstances[elementId]) {
        window.editorInstances[elementId] = null;
    }

    const quill = new Quill(`#${elementId}`, {
        modules: {
            toolbar: toolbarOptions
        },
        theme: 'snow',
        placeholder: 'اكتب المحتوى هنا...',
        direction: 'rtl'
    });

    // Store the editor instance
    window.editorInstances[elementId] = quill;

    // Sync content with hidden textarea
    const textareaId = elementId === 'editor' ? 'documentContent' : 'editTemplateContent';

    quill.on('text-change', function () {
        const content = quill.root.innerHTML;
        const textarea = document.getElementById(textareaId);
        if (textarea) {
            textarea.value = content;
            const event = new Event('change', { bubbles: true });
            textarea.dispatchEvent(event);
        }
    });

    console.log(`Quill Editor initialized for ${elementId}`);
};

// Fix: Add the missing setQuillContent function
window.setQuillContent = function (elementId, content) {
    if (window.editorInstances[elementId]) {
        window.editorInstances[elementId].root.innerHTML = content || '';
    } else {
        // If editor not initialized yet, set content when it initializes
        console.warn(`Editor ${elementId} not initialized yet. Content will be set on initialization.`);
        setTimeout(() => {
            if (window.editorInstances[elementId]) {
                window.editorInstances[elementId].root.innerHTML = content || '';
            }
        }, 100);
    }
};

// Fix: Add the missing getQuillContent function
window.getQuillContent = function (elementId) {
    if (window.editorInstances[elementId]) {
        return window.editorInstances[elementId].root.innerHTML;
    }
    return '';
};

// Fix: Add the missing clearQuillEditor function
window.clearQuillEditor = function (elementId) {
    if (window.editorInstances[elementId]) {
        window.editorInstances[elementId].root.innerHTML = '';
    }
};