const heading = document.getElementById('head1')

setTimeout(() => {
    addStylesTo(heading, 'Информация о волонтерах', 'black', '2rem', 'center')
}, 1500)

function addStylesTo(node, text, color = 'black', fontSize, textAlign) {
    node.textAlign = textAlign
    node.color = color
    node.text = text
    node.fontSize = fontSize

}