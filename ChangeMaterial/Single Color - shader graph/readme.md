## object & collider
- create object (ex: cube) name it "color object"
- create emptyObject (as collider) name it "collider"

## material
- create CubeMaterial
- create CubeLitShader
- drag CubeLitShader into CubeMaterial

## Lit Shader
- in Shader Graph (Blackboard), press + (plus button)
- create CubeColor (type color)
- create TargetColor (type color)

### Cube Color
- click Cube Color, in Graph Inspector click exposed (true)
- in graph inspector (Name: CubeColor, Reference: _CubeColor, Default: red)
- drag CubeColor from ShaderGraph and connect it to BaseColor

### Target Color
- click Cube Color, in Graph Inspector click exposed (true)
- in graph inspector (Name: TargetColor, Reference: _TargetColor, Default: blue/whatever)


## assign
- attach CubeMaterial to color object
- attach script to collider
- drag color object into collider script(Object Render input)

## Color Object
- click Color Object, can change CubeColor
- click Color Object, can change TargetColor

## player
- tag == "Player" -> collider in = TargetColor
- tag == "Player" -> collider out = CubeColor (originalMaterial)