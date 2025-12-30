## object & collider
- create object (ex: cube)
- create emptyObject (as collider) name it "color object"

## material
- create material1 & material2
- create litShader1 & litShader2
- drag litShader1 into material1 (red)
- drag litShader2 into material2 (blue)

## assign
- attach material1 to object(cube)
- attach script to collider (color object)
- drag color object into script(Object Render input)
- drag material2 into script(Material input)

## player
- tag == "Player" -> collider in = blue
- tag == "Player" -> collider out = red (originalMaterial)