struct VOut
{
	float4 position : SV_POSITION;
	float4 color    : COLOR;
};

cbuffer Ratio : register(b0){
	float4x4 ratio;
}

cbuffer InvertCamera : register(b1) {
	float4x4 camara;
}

VOut main(float4 position : POSITION, float4 color : COLOR)
{
	VOut output;

	//scaling according to the z achse
	float4 p = float4(position[0] / (position[2] + 0.01f), position[1] / (position[2] + 0.01f), position[2] - abs(position[2]), position[3]);
	p = mul(ratio, p);
	//setting the values for the pixelshader
	output.position = mul(camara,p);
	output.color = color;

	return output;
}